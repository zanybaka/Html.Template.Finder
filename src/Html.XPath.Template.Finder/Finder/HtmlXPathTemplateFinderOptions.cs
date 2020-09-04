namespace Html.Template.Finder
{
    public class HtmlXPathTemplateFinderOptions : HtmlTemplateFinderOptions
    {
        public bool ReadTemplateOnInit;

        public HtmlXPathTemplateFinderOptions(bool readTemplateOnInit = false, bool skipEmptyEntities = false, bool trimValues = false, bool cleanHtml = true)
            : base(skipEmptyEntities, trimValues, cleanHtml)
        {
            ReadTemplateOnInit = readTemplateOnInit;
        }
    }
}