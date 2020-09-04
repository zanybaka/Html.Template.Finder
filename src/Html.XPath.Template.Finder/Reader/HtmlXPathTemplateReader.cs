using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Shared.Utils.Lib.Entities.String;
using Shared.Utils.Lib.Extensions;

namespace Html.Template.Finder
{
    public class HtmlXPathTemplateReader : HtmlTemplateReaderBase<HtmlXPathTemplate, HtmlTemplateOptions>
    {
        public HtmlXPathTemplateReader(string rawTemplate, HtmlTemplateOptions options) : base(rawTemplate, options) { }

        public override HtmlXPathTemplate Read()
        {
            HtmlXPathTemplate template = new HtmlXPathTemplate();
            string[] lines =
                new SplitText(
                    RawTemplate,
                    StringSplitOptions.RemoveEmptyEntries,
                    Options.LineEnding);
            template.RootNodeXPath = lines[0];
            List<HtmlXPathTemplatePattern> list = new List<HtmlXPathTemplatePattern>();
            HtmlXPathTemplatePattern lastPattern = null;
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                HtmlXPathTemplatePattern pattern = new HtmlXPathTemplatePattern();
                {
                    pattern.Level = line
                        .SplitByLength(Options.Tabulator.Length)
                        .TakeWhile(x => x == Options.Tabulator)
                        .Count();
                }
                {
                    Regex innerTextRegex = new Regex(@"\/\$(?<innerText>\w+)$");
                    Match innerTextMatch = innerTextRegex.Match(line);
                    if (innerTextMatch.Success)
                    {
                        pattern.InnerTextVariable = innerTextMatch.Groups["innerText"].Value;
                        line = line.Substring(0, line.Length - pattern.InnerTextVariable.Length - 2);
                    }
                }
                {
                    Regex attributeRegex = new Regex(@"@(?<from>[^=]+)=\$(?<to>\w+)");
                    MatchCollection attributeMatches = attributeRegex.Matches(line);
                    foreach (Match attributeMatch in attributeMatches)
                    {
                        if (attributeMatch.Success)
                        {
                            pattern.AddAttributeVariable(
                                attributeMatch.Groups["from"].Value,
                                attributeMatch.Groups["to"].Value);
                            line = attributeRegex.Replace(line, m => $"@{m.Groups["from"].Value}", 1);
                        }
                    }
                }
                {
                    pattern.XPathSelector = line.Substring(Options.Tabulator.Length * pattern.Level);
                }

                if (pattern.Level == 1)
                {
                    list.Add(pattern);
                }
                else
                {
                    bool found = false;
                    HtmlXPathTemplatePattern potentialParent = lastPattern;
                    while (potentialParent != null)
                    {
                        if (potentialParent.Level == pattern.Level - 1)
                        {
                            found = true;
                            pattern.Parent = potentialParent;
                            potentialParent.AddChild(pattern);
                            break;
                        }

                        potentialParent = potentialParent.Parent;
                    }

                    if (!found)
                    {
                        throw new InvalidDataException(
                            $"Parent could not be found for the pattern with index={i}. Expected parent level is {pattern.Level - 1}. XPathSelector={pattern.XPathSelector}");
                    }
                }

                lastPattern = pattern;
            }

            template.Patterns = list.ToArray();
            return template;
        }
    }
}