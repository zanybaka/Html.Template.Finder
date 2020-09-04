using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Html.Template.Finder.Tests
{
    [TestFixture]
    public class HtmlXPathTemplateReaderFixture
    {
        [Test]
        public void HtmlXPathTemplateReaderTest()
        {
            HtmlXPathTemplate template =
                new HtmlXPathTemplateReader(
                    File.ReadAllText(Constants.MarketYandexXPathTemplateFile),
                    HtmlTemplateOptions.Default)
                .Read();

            template.RootNodeXPath.Should().Be("//article[@data-zone-name='snippet-card']");
            template.Patterns.Length.Should().Be(5);

            template.Patterns[0].Parent.Should().BeNull();
            template.Patterns[0].GetChildren().Should().BeEmpty();
            template.Patterns[0].Level.Should().Be(1);
            template.Patterns[0].XPathSelector.Should().Be(".//img[@src]");
            template.Patterns[0].GetAttributeVariables().Should().BeEquivalentTo(new KeyValuePair<string, string>("src", "img"));
            template.Patterns[0].InnerTextVariable.Should().BeNullOrEmpty();

            template.Patterns[1].Parent.Should().BeNull();
            template.Patterns[1].GetChildren().Length.Should().Be(1);
            template.Patterns[1].Level.Should().Be(1);
            template.Patterns[1].XPathSelector.Should().Be(".//*[@data-zone-name='title']");
            template.Patterns[1].GetAttributeVariables().Should().BeNullOrEmpty();
            template.Patterns[1].InnerTextVariable.Should().BeNullOrEmpty();
            HtmlXPathTemplatePattern child1 = template.Patterns[1].GetChildren().First();
            child1.Parent.Should().Be(template.Patterns[1]);
            child1.GetChildren().Should().BeEmpty();
            child1.Level.Should().Be(2);
            child1.XPathSelector.Should().Be(".//a[@href and @title]");
            child1.GetAttributeVariables().Length.Should().Be(2);
            child1.GetAttributeVariables()[0].Should().BeEquivalentTo(new KeyValuePair<string, string>("href", "url"));
            child1.GetAttributeVariables()[1].Should().BeEquivalentTo(new KeyValuePair<string, string>("title", "title"));
            child1.InnerTextVariable.Should().BeNullOrEmpty();

            template.Patterns[2].GetChildren().Length.Should().Be(1);
            HtmlXPathTemplatePattern child2 = template.Patterns[2].GetChildren().First();
            child2.Parent.Should().Be(template.Patterns[2]);
            child2.GetChildren().Should().BeEmpty();
            child2.Level.Should().Be(2);
            child2.XPathSelector.Should().Be(".//span");
            child2.GetAttributeVariables().Should().BeEmpty();
            child2.InnerTextVariable.Should().Be("rating");

            template.Patterns[3].InnerTextVariable.Should().Be("price");

            template.Patterns[4].InnerTextVariable.Should().Be("count");
        }
    }
}