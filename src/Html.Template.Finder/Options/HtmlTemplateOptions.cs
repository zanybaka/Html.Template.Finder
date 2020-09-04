using System;

namespace Html.Template.Finder
{
    public class HtmlTemplateOptions
    {
        public string LineEnding;
        public string Tabulator;

        public HtmlTemplateOptions(string tabulator, string lineEnding)
        {
            Tabulator = tabulator;
            LineEnding = lineEnding;
        }

        public static HtmlTemplateOptions Default => new HtmlTemplateOptions(tabulator: "\t", lineEnding: Environment.NewLine);
    }
}