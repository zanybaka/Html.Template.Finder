using System;
using HtmlAgilityPack;

namespace Shared.Utils.Web.Entities.Html
{
    public class SelectFirstChildNode
    {
        private readonly HtmlNode _node;
        private readonly string _xPathSelector;

        public SelectFirstChildNode(HtmlNode node, string xPathSelector)
        {
            if (xPathSelector == null || !xPathSelector.StartsWith(".//"))
            {
                throw new ArgumentException("xPathSelector must be started from .//");
            }
            _node = node;
            _xPathSelector = xPathSelector;
        }

        public static implicit operator HtmlNode(SelectFirstChildNode obj)
        {
            return obj.GetValue();
        }

        public HtmlNode GetValue()
        {
            return _node.SelectSingleNode(_xPathSelector);
        }

        public override string ToString()
        {
            return _xPathSelector;
        }
    }
}
