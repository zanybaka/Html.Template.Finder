using System.Collections.Generic;

namespace Html.Template.Finder
{
    public interface IHtmlTemplateFinder<out TEntity>
    {
        IEnumerable<TEntity> FindEntities(string html);
    }
}