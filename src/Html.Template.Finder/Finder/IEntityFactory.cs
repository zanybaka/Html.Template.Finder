namespace Html.Template.Finder
{
    public interface IEntityFactory<out TEntity>
    {
        TEntity Create(string value);
    }
}