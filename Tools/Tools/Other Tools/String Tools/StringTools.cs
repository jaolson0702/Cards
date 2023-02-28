using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class StringTools
    {
        public static string Capitalized(this string given) => given.ToUpper(0);

        public static int ToInt(this string given) => Convert.ToInt32(given);

        public static double ToDouble(this string given) => Convert.ToDouble(given);

        public static string[] ToArray(this string given)
        {
            List<string> result = new();
            foreach (char c in given.ToCharArray()) result.Add(c.ToString());
            return result.ToArray();
        }

        public static char ToChar(this string given) => given[0];

        public static bool ToBoolean(this string given) => Convert.ToBoolean(given);

        public static string Join(this object a, object b, string join = "") => $"{a}{join}{b}";

        public static bool Contains(this string given, char element) => given.ToCharArray().Contains(element);

        public static bool ContainsIC(this string given, char element) => given.Contains(element.ToUpper()) || given.Contains(element.ToLower());

        public static bool EqualsIC(this string given, string other) => given.ToUpper() == other.ToUpper();

        public static Substring[] Find(this string given, string target)
        {
            List<Substring> result = new();
            for (int a = 0; a < given.Length; a++)
                for (int b = a; b < given.Length; b++)
                    if (given.Substring(a, b) == target)
                        result.Add(new(a, b));
            return result.ToArray();
        }

        public static Substring[] FindIC(this string given, string target)
        {
            List<Substring> result = new();
            for (int a = 0; a < given.Length; a++)
                for (int b = a; b < given.Length; b++)
                    if (given.Substring(a, b).EqualsIC(target))
                        result.Add(new(a, b));
            return result.ToArray();
        }

        public static string ToUpper(this string given, int index) => ConvertCase(given, true, index);

        public static string ToLower(this string given, int index) => ConvertCase(given, false, index);

        public static string ToTitle(this string given)
        {
            string[] array = given.Trim().Split(" ").Mapped(str => str.Capitalized());
            string result = string.Empty;
            for (int a = 0; a < given.LeadingSpaces(); a++) result += " ";
            array.ForEach(str => result += str + " ");
            for (int a = 0; a < given.TrailingSpaces(); a++) result += " ";
            if (given.TrailingSpaces() == 0 && result[^1] == ' ') result = result.Backspace();
            return result;
        }

        public static int LeadingSpaces(this string given)
        {
            int result = 0;
            while (given.ToCharArray()[result] == ' ') result++;
            return result;
        }

        public static int TrailingSpaces(this string given)
        {
            int result;
            int index = given.Length - 1;
            for (result = 0; given.ToCharArray()[index] == ' '; index--) result++;
            return result;
        }

        public static string Backspace(this string given) => given[..^1];

        public static StringCaseMap CaseMap(this string given)
        {
            List<Tuple<int, bool?>> settings = new();
            given.ToCharArray().ForEachIndex(index =>
            {
                char c = given.ToCharArray()[index];
                if (c.IsLetter()) settings.Add(new(index, c == c.ToUpper()));
            });
            return new(settings.ToArray());
        }

        public static string AppliedRandomCaseMap(this string given) => StringCaseMap.GenerateRandom(given.Length).AppliedTo(given);

        private static string ConvertCase(string given, bool toUpper, int index)
        {
            string result = string.Empty;
            given.ToCharArray().ForEachIndex(i =>
            {
                if (i == index)
                {
                    Func<char, char> func = toUpper ? char.ToUpper : char.ToLower;
                    result += func(given[i]);
                }
                else result += given[i];
            });
            return result;
        }
    }
}