using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Html.Template.Finder.Tests
{
    [TestFixture]
    public class AvitoHtmlXPathTemplateFinderFixture
    {
        [Test]
        public void FindEntitiesTest()
        {
            AvitoEntity[] entities =
                new HtmlXPathTemplateFinder<AvitoEntity>(
                    new HtmlXPathTemplateReader(
                        File.ReadAllText(Constants.AvitoXPathTemplateFile),
                        new HtmlTemplateOptions(
                            tabulator: "    ",
                            Environment.NewLine)),
                    new JsonEntityFactory<AvitoEntity>(), 
                    new HtmlXPathTemplateFinderOptions(trimValues: true, readTemplateOnInit: true))
                .FindEntities(File.ReadAllText(Constants.AvitoHtmlFile))
                .ToArray();

            entities.Length.Should().Be(50);

            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                entity.url.Should().NotBeNullOrEmpty();
                entity.img.Should().NotBeNullOrEmpty();
                entity.price.Should().NotBeNullOrEmpty();
                entity.title.Should().NotBeNullOrEmpty();
                entity.category.Should().NotBeNullOrEmpty();
                entity.address.Should().NotBeNullOrEmpty();
                entity.date.Should().NotBeNullOrEmpty();
            }

            VerifyAnEntity(entities.First());
        }

        private static void VerifyAnEntity(AvitoEntity entity)
        {
            entity.url.Should().Be("/chasy_i_ukrasheniya/apple_watch_5_kopiya_vysokogo_kachestva_2008604170");
            entity.address.Should().Be("Гостиный двор");
            entity.category.Should().Be("Часы и украшения");
            entity.date.Should().Be("1 день назад");
            entity.img.Should().Be("https://00.img.avito.st/208x156/9148229700.jpg");
            entity.price.Should().Be("2 499  ₽");
            entity.title.Should().Be("Apple watch 5 копия высокого качества");
        }
    }
}
