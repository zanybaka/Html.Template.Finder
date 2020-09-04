using System.Diagnostics;

namespace Html.Template.Finder.Tests
{
    [DebuggerDisplay("{title} {price}")]
    public struct MarketYandexEntity
    {
        public string img;
        public string url;
        public string title;
        public string count;
        public string price;
        public string rating;
    }
}