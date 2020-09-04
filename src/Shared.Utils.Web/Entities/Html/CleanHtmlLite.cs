using System;
using System.Text.RegularExpressions;

namespace Shared.Utils.Web.Entities.Html
{
    public class CleanHtmlLite
    {
        private readonly string _html;
        private readonly Lazy<string> _cleanHtml;

        public CleanHtmlLite(string html, Func<bool> condition)
        {
            _html = html;
            _cleanHtml = new Lazy<string>(() => CleanHtml(_html, condition));
        }

        public static implicit operator string(CleanHtmlLite obj)
        {
            return obj._cleanHtml.Value;
        }

        public override string ToString()
        {
            return _html;
        }

        private string CleanHtml(string html, Func<bool> condition)
        {
            if (!condition())
            {
                return html;
            }

            var regex = new Regex("<!-- .*?-->");
            html = regex.Replace(html, "");
            html = html.Replace("&nbsp;", " ");
            return html;
        }
    }
}
