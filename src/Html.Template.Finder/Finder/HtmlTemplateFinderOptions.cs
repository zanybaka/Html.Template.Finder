namespace Html.Template.Finder
{
    public class HtmlTemplateFinderOptions
    {
        public bool TrimValues;
        public bool CleanHtml;
        public bool SkipEmptyEntities;

        public HtmlTemplateFinderOptions(bool skipEmptyEntities = false, bool trimValues = false, bool cleanHtml = true)
        {
            SkipEmptyEntities = skipEmptyEntities;
            CleanHtml = cleanHtml;
            TrimValues = trimValues;
        }
    }
}