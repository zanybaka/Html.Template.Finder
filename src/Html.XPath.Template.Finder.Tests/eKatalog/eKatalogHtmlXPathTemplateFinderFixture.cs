using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Html.Template.Finder.Tests
{
    [TestFixture]
    public class eKatalogHtmlXPathTemplateFinderFixture
    {
        [Test]
        public void FindEntitiesTest()
        {
            eKatalogEntity[] entities =
                new HtmlXPathTemplateFinder<eKatalogEntity>(
                    new HtmlXPathTemplateReader(
                        File.ReadAllText(Constants.eKatalogXPathTemplateFile),
                        HtmlTemplateOptions.Default),
                    new JsonEntityFactory<eKatalogEntity>(), 
                    new HtmlXPathTemplateFinderOptions(trimValues: true))
                .FindEntities(File.ReadAllText(Constants.eKatalogHtmlFile))
                .ToArray();

            entities.Length.Should().Be(7);

            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                entity.url.Should().NotBeNullOrEmpty($"(Index = {i})");
                entity.img.Should().NotBeNullOrEmpty($"(Index = {i})");
                entity.price.Should().NotBeNullOrEmpty($"(Index = {i})");
                entity.title.Should().NotBeNullOrEmpty($"(Index = {i})");
                entity.description.Should().NotBeNullOrEmpty($"(Index = {i})");
            }

            VerifyAnEntity(entities.First());
        }

        private static void VerifyAnEntity(eKatalogEntity entity)
        {
            entity.img.Should().Be("/jpg/935778.jpg");
            entity.url.Should().Be("/THERMO-THERMOREG-TI-300.htm");
            entity.title.Should().Be("Терморегулятор Thermo Thermoreg TI-300");
            entity.description.Should().Be("для обогревателя / теплого пола, в монтажную коробку, проводной, на 3600 Вт");
            entity.price.Should().Be("4 089");
            entity.count.Should().Be("6");
        }
    }
}
