using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using HtmlAgilityPack;
using Shared.Utils.Lib.Entities.If;
using Shared.Utils.Lib.Entities.Json;
using Shared.Utils.Lib.Entities.String;
using Shared.Utils.Web.Entities.Html;

namespace Html.Template.Finder
{
    public class HtmlXPathTemplateFinder<TEntity> : IHtmlTemplateFinder<TEntity>
    {
        private readonly IEntityFactory<TEntity> _entityFactory;
        private readonly HtmlXPathTemplateFinderOptions _options;
        private readonly Lazy<HtmlXPathTemplate> _template;

        public HtmlXPathTemplateFinder(
            IHtmlTemplateReader<HtmlXPathTemplate> reader,
            IEntityFactory<TEntity> entityFactory,
            HtmlXPathTemplateFinderOptions options)
        {
            _entityFactory = entityFactory;
            _options = options;
            _template = new Lazy<HtmlXPathTemplate>(reader.Read);
            if (_options.ReadTemplateOnInit)
            {
                var warmup = _template.Value;
            }
        }

        public IEnumerable<TEntity> FindEntities(string html)
        {
            HtmlNode documentNode =
                new HtmlToHtmlNode(
                    new CleanHtmlLite(
                        html,
                        () => _options.CleanHtml));
            HtmlXPathTemplate template = _template.Value;
            HtmlNodeCollection nodes = documentNode.SelectNodes(template.RootNodeXPath);
            foreach (HtmlNode node in nodes)
            {
                JsonObject json = new JsonObject();
                FillJsonProperties<TEntity>(template.Patterns, node, json);
                if (!_options.SkipEmptyEntities || !json.IsEmpty)
                {
                    yield return _entityFactory.Create(json.ToString());
                }
            }
        }

        private void FillJsonProperties<TEntity>(HtmlXPathTemplatePattern[] patterns, HtmlNode node, JsonObject json)
        {
            foreach (HtmlXPathTemplatePattern rawPattern in patterns)
            {
                HtmlNode childNode;
                try
                {
                    childNode =
                        new Iif<HtmlNode>(
                            () => new IsEmptyString(rawPattern.XPathSelector),
                            node,
                            new SelectFirstChildNode(
                                node,
                                rawPattern.XPathSelector));
                }
                catch (XPathException e)
                {
                    throw new InvalidDataException($"{rawPattern.XPathSelector}", e);
                }

                if (childNode != null)
                {
                    if (!new IsEmptyString(rawPattern.InnerTextVariable))
                    {
                        json.AddKeyValue(
                            rawPattern.InnerTextVariable,
                            new Iif<string>(
                                () => _options.TrimValues,
                                new TrimText(childNode.InnerText),
                                childNode.InnerText ?? ""));
                    }

                    if (rawPattern.HasAttributeVariables)
                    {
                        var pairs = rawPattern.GetAttributeVariables();
                        foreach (KeyValuePair<string, string> pair in pairs)
                        {
                            string from = pair.Key;
                            string to = pair.Value;
                            json.AddKeyValue(
                                to,
                                new Iif<string>(
                                    () => _options.TrimValues,
                                    new TrimText(childNode.Attributes[from].Value),
                                    childNode.Attributes[from].Value ?? ""));
                        }
                    }

                    if (rawPattern.HasChildren)
                    {
                        FillJsonProperties<TEntity>(rawPattern.GetChildren(), childNode, json);
                    }
                }
            }
        }
    }
}