namespace Html.Template.Finder
{
    public interface IHtmlTemplateReader<out TTemplate>
        where TTemplate : IHtmlTemplate
    {
        TTemplate Read();
    }
}