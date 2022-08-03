using System.Linq;

namespace SE.Core.Utils
{
    public static class StringUtils
    {
        public static string NumbersOnly(this string str, string input) => new(input.Where(char.IsDigit).ToArray());
    }
}
