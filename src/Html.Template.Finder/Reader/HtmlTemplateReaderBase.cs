namespace Html.Template.Finder
{
    public abstract class HtmlTemplateReaderBase<TTemplate, TOptions> : IHtmlTemplateReader<TTemplate>
        where TTemplate : IHtmlTemplate
        where TOptions : HtmlTemplateOptions
    {
        protected readonly string RawTemplate;
        protected readonly TOptions Options;

        protected HtmlTemplateReaderBase(string rawTemplate, TOptions options)
        {
            RawTemplate = rawTemplate;
            Options = options;
        }

        public abstract TTemplate Read();
    }
}