using Shared.Utils.Lib.Entities.Json;

namespace Html.Template.Finder
{
    public class JsonEntityFactory<TEntity> : IEntityFactory<TEntity>
    {
        public TEntity Create(string value)
        {
            return new EntityFromJson<TEntity>(value);
        }
    }
}