using System;

namespace Shared.Utils.Lib.Entities.String
{
    public class SplitText
    {
        private readonly string _separator;
        private readonly StringSplitOptions _options;
        private readonly string _input;

        public SplitText(string input, StringSplitOptions options, string separator = " ")
        {
            _separator = separator;
            _options = options;
            _input = input ?? "";
        }

        public static implicit operator string[](SplitText obj)
        {
            return obj.GetValue();
        }

        public string[] GetValue()
        {
            return _input.Split(new [] { _separator }, _options);
        }
    }
}