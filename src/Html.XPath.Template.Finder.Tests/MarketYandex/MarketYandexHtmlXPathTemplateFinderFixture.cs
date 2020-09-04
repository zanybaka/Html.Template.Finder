using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Shared.Utils.Lib.Extensions;

namespace Html.Template.Finder.Tests
{
    [TestFixture]
    public class MarketYandexHtmlXPathTemplateFinderFixture
    {
        [Test]
        public void FindEntitiesTest1()
        {
            MarketYandexEntity[] entities =
                CreateInstance()
                    .FindEntities(File.ReadAllText(Constants.MarketHtmlFile1))
                    .ToArray();

            entities.Length.Should().Be(48);

            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                entity.url.Should().NotBeNullOrEmpty($"Entity {i} does not contain 'url'");
                entity.img.Should().NotBeNullOrEmpty();
                entity.title.Should().NotBeNullOrEmpty();
            }

            VerifyAnEntity(entities.Second());
        }
        
        [Test]
        public void FindEntitiesTest2()
        {
            MarketYandexEntity[] entities =
                CreateInstance()
                    .FindEntities(File.ReadAllText(Constants.MarketHtmlFile2))
                    .ToArray();

            entities.Length.Should().Be(29);

            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                entity.url.Should().NotBeNullOrEmpty($"Entity {i} does not contain 'url'");
                entity.img.Should().NotBeNullOrEmpty();
                entity.title.Should().NotBeNullOrEmpty();
                entity.price.Should().NotBeNullOrEmpty();
                entity.count.Should().NotBeNullOrEmpty();
            }
        }

        private static void VerifyAnEntity(MarketYandexEntity entity)
        {
            entity.img.Should().Be("//avatars.mds.yandex.net/get-mpic/1215212/img_id2615183774357289607.jpeg/6hq");
            entity.url.Should().Be("/product--termoreguliator-thermo-thermoreg-ti-970/41449445?nid=71860&amp;show-uid=15989786419501928277916002&amp;context=search&amp;text=thermoreg");
            entity.title.Should().Be("Терморегулятор Thermo Thermoreg TI-970");
            entity.price.Should().Be("6 317 ₽");
            entity.rating.Should().Be("4.0");
            entity.count.Should().Be("45 предложений");
        }

        private static IHtmlTemplateFinder<MarketYandexEntity> CreateInstance()
        {
            return new HtmlXPathTemplateFinder<MarketYandexEntity>(
                new HtmlXPathTemplateReader(
                    File.ReadAllText(Constants.MarketYandexXPathTemplateFile),
                    HtmlTemplateOptions.Default),
                new JsonEntityFactory<MarketYandexEntity>(),
                new HtmlXPathTemplateFinderOptions(trimValues: true));
        }
    }
}
