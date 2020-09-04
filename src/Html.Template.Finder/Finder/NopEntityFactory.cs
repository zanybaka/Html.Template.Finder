namespace Html.Template.Finder
{
    public class NopEntityFactory : IEntityFactory<string>
    {
        public string Create(string value)
        {
            return value;
        }
    }
}