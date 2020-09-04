using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Html.Template.Finder.Tests
{
    [TestFixture]
    public class HtmlXPathTemplateFinderFixture
    {
        [Test]
        public void FindEntitiesTest()
        {
            string[] entities =
                new HtmlXPathTemplateFinder<string>(
                    new HtmlXPathTemplateReader(
                        File.ReadAllText(Constants.TestXPathTemplateFile),
                        new HtmlTemplateOptions(
                            tabulator: "\t",
                            Environment.NewLine)),
                    new NopEntityFactory(), 
                    new HtmlXPathTemplateFinderOptions(trimValues: true, readTemplateOnInit: true, skipEmptyEntities: true))
                .FindEntities(File.ReadAllText(Constants.HtmlFile))
                .ToArray();

            entities.Length.Should().Be(2);
            entities[0].Should().Be("{\r\n\t\"innerText\": \"1\",\r\n\t\"attribute\": \"one\",\r\n}");
            entities[1].Should().Be("{\r\n\t\"innerText\": \"4\",\r\n\t\"attribute\": \"four\",\r\n}");
        }
    }
}
