using System.Collections.Generic;
using System.Linq;

namespace Html.Template.Finder
{
    public class HtmlXPathTemplatePattern
    {
        private readonly List<HtmlXPathTemplatePattern> _children = new List<HtmlXPathTemplatePattern>();
        private readonly Dictionary<string, string> _attributeVariables = new Dictionary<string, string>();

        public HtmlXPathTemplatePattern Parent;
        public string XPathSelector;
        public int Level;
        public string InnerTextVariable;

        public bool HasChildren => _children.Count > 0;
        public bool HasAttributeVariables => _attributeVariables.Count > 0;
        public HtmlXPathTemplatePattern[] GetChildren() => _children.ToArray();
        public KeyValuePair<string, string>[] GetAttributeVariables() => _attributeVariables.ToArray();
        public void AddChild(HtmlXPathTemplatePattern pattern) => _children.Add(pattern);
        public void AddAttributeVariable(KeyValuePair<string, string> pair) => _attributeVariables.Add(pair.Key, pair.Value);
        public void AddAttributeVariable(string from, string to) => AddAttributeVariable(new KeyValuePair<string, string>(from, to));
    }
}