using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class StringCaseMap
    {
        public static readonly StringCaseMap InitialUpper = new(true);

        private readonly List<Tuple<int, bool?>> settings = new();

        public StringCaseMap(params bool?[] given)
        {
            for (int a = 0; a < given.Length; a++)
                settings.Add(new(a, given[a]));
        }

        public StringCaseMap(int index, bool given)
            => settings.Add(new(index, given));

        public StringCaseMap(params Tuple<int, bool?>[] given)
        {
            foreach (Tuple<int, bool?> tuple in given)
                settings.Add(tuple);
        }

        public Tuple<int, bool?>[] Settings => settings.ToArray();

        public string AppliedTo(string given)
        {
            string[] givenSplit = given.ToCharArray().Mapped(c => c.ToString());
            string[] arrayResult = new string[givenSplit.Length];
            Array.Copy(givenSplit, arrayResult, givenSplit.Length);
            for (int a = 0; a < givenSplit.Length; a++)
            {
                foreach (Tuple<int, bool?> tuple in Settings)
                {
                    if (tuple.Item1 == a)
                    {
                        arrayResult[a] = tuple.Item2 switch
                        {
                            true => givenSplit[a].ToUpper(),
                            false => givenSplit[a].ToLower(),
                            null => givenSplit[a]
                        };
                    }
                }
            }
            string result = string.Empty;
            arrayResult.ForEach(s => result += s);
            return result;
        }

        public static StringCaseMap GenerateRandom(int length)
        {
            List<bool?> settings = new();
            for (int a = 0; a < length; a++) settings.Add(new bool?[] { true, false, null }.GetRandomElement());
            return new(settings.ToArray());
        }

        public static StringCaseMap operator !(StringCaseMap given) => new(given.Settings.Mapped<Tuple<int, bool?>, Tuple<int, bool?>>(tuple => new(tuple.Item1, tuple.Item2.Opposite())).ToArray());
    }
}