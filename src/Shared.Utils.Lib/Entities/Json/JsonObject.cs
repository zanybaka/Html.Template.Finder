using System;
using System.Text;

namespace Shared.Utils.Lib.Entities.Json
{
    public class JsonObject
    {
        private readonly StringBuilder _sb;
        private bool _isEmpty;

        public JsonObject()
        {
            _sb = new StringBuilder();
            _isEmpty = true;
        }

        public bool IsEmpty => _isEmpty;

        public static implicit operator string(JsonObject obj)
        {
            return obj.ToString();
        }

        public override string ToString()
        {
            return $"{{{Environment.NewLine}{_sb}}}";
        }

        public void AddKeyValue(string key, string rawValue)
        {
            string value = System.Web.HttpUtility.JavaScriptStringEncode(rawValue);
            _sb.AppendLine($"\t\"{key}\": \"{value}\",");
            _isEmpty = false;
        }
    }
}