using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public static class CollectionTools
    {
        public static IEnumerable<T> WithValuesThatSatisfy<T>(this IEnumerable<T> given, Predicate<T> predicate)
        {
            List<T> result = new();
            given.ForEachThatSatisfies(predicate, element => result.Add(element));
            return result;
        }

        public static List<T> ToList<T>(this T[] given) => new(given);

        public static List<T> Sorted<T>(this IEnumerable<T> given)
        {
            List<T> result = new(given);
            result.Sort();
            return result;
        }

        public static List<T> Sorted<T>(this IEnumerable<T> given, IComparer<T> comparer)
        {
            List<T> result = new(given);
            result.Sort(comparer);
            return result;
        }

        public static List<T> Sorted<T>(this IEnumerable<T> given, Func<T, T, int> comparer)
        {
            List<T> result = new(given);
            for (int a = 0; a < result.Count; a++)
            {
                for (int b = a; b < result.Count; b++)
                {
                    if (comparer(result[a], result[b]) > 0)
                    {
                        T oldA = result[a], oldB = result[b];
                        (result[a], result[b]) = (oldB, oldA);
                    }
                }
            }
            return result;
        }

        public static T[] Reversed<T>(this T[] given)
        {
            List<T> result = new(given);
            result.Reverse();
            return result.ToArray();
        }

        public static List<T> Reversed<T>(this IEnumerable<T> given) => given.ToArray().Reversed().ToList();

        public static List<T> Scrambled<T>(this T[] given)
        {
            List<T> unusedElements = new(given);
            List<T> result = new();
            for (int a = 0; a < given.Length; a++)
            {
                int index = unusedElements.GetRandomIndex();
                result.Add(unusedElements[index]);
                unusedElements.RemoveAt(index);
            }
            return result;
        }

        public static List<T> Scrambled<T>(this IEnumerable<T> given) => given.ToArray().Scrambled();

        public static List<T> JoinedWith<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            List<T> result = new(a);
            result.AddRange(b);
            return result;
        }

        public static List<T> JoinedWith<T>(this T a, IEnumerable<T> b) => new T[] { a }.JoinedWith(b);

        public static void ForEach<T>(this T[] array, Action<T> action) => array.ToList().ForEach(action);

        public static void ForEach<T>(this IEnumerable<T> given, Action<T> action) => given.ToList().ForEach(action);

        public static void ForEachIndex<T>(this List<T> list, Action<int> action)
        { for (int a = 0; a < list.Count; a++) action(a); }

        public static void ForEachIndex<T>(this T[] array, Action<int> action)
        { for (int a = 0; a < array.Length; a++) action(a); }

        public static string ToString<T>(this IEnumerable<T> given, string separator) => given.ToString("", separator, "", "");

        public static string ToString<T>(this IEnumerable<T> given, string heading, string separator, string nextToEnd, string end)
        {
            string result = heading;
            for (int a = 0; a < given.ToList().Count; a++)
                result += (a == given.ToList().Count - 1 ? nextToEnd : "") + given.ToList()[a] + (a == given.ToList().Count - 1 ? "" : separator);
            return result + end;
        }

        public static string ToString<T>(this IEnumerable<T> given, string heading, string marker, string separator, string nextToEnd, string end)
        {
            string result = heading;
            for (int a = 0; a < given.ToList().Count; a++)
                result += $"{(a == given.ToList().Count - 1 ? nextToEnd : "")}{a + 1}{marker}{given.ToList()[a]}{(a == given.ToList().Count - 1 ? "" : separator)}";
            return result + end;
        }

        public static string ToString<T, U>(this IEnumerable<T> a, IEnumerable<U> b, string heading, string between, string separator, string nextToEnd, string end)
        {
            if (a.ToList().Count != b.ToList().Count) throw new IndexOutOfRangeException("The two specified collections must have the same number of elements.");
            string result = heading;
            for (int c = 0; c < a.ToList().Count; c++)
                result += $"{(c == a.ToList().Count - 1 ? nextToEnd : "")}{a.ToList()[c]}{between}{b.ToList()[c]}{(c == a.ToList().Count - 1 ? "" : separator)}";
            return result + end;
        }

        public static string ToString<T, U>(this IEnumerable<T> a, IEnumerable<U> b, string heading, string marker, string between, string separator, string nextToEnd, string end)
        {
            if (a.ToList().Count != b.ToList().Count) throw new IndexOutOfRangeException("The two specified collections must have the same number of elements.");
            string result = heading;
            for (int c = 0; c < a.ToList().Count; c++)
                result += $"{(c == a.ToList().Count - 1 ? nextToEnd : "")}{c + 1}{marker}{a.ToList()[c]}{between}{b.ToList()[c]}{(c == a.ToList().Count - 1 ? "" : separator)}";
            return result + end;
        }

        public static int IndexOf<T>(this T[] given, T element) => Array.IndexOf(given, element);

        public static int LastIndexOf<T>(this T[] given, T element) => Array.LastIndexOf(given, element);

        public static int LastIndexOf<T>(this T[] given, T element, int index) => Array.LastIndexOf(given, element, index);

        public static int LastIndexOf<T>(this T[] given, T element, int start, int end) => Array.LastIndexOf(given, element, start, end);

        public static bool IsEqualTo<T>(this T[] given, T[] other) => given.TrueForAllIndices(index => given[index].Equals(other[index]));

        public static bool IsEqualTo<T>(this IEnumerable<T> given, IEnumerable<T> other) => given.ToArray().IsEqualTo(other.ToArray());

        public static bool Contains<T>(this IEnumerable<T> given, Predicate<T> predicate) => given.ToList().Find(predicate) is not null;

        public static bool Contains<T>(this T[] given, T element) => given.ToList().Contains(element);

        public static bool IsIn<T>(this T element, params T[] given) => given.Contains(element);

        public static bool IsIn<T>(this T element, IEnumerable<T> given) => element.IsIn(given.ToArray());

        public static int IndexIn<T>(this T element, params T[] given) => given.IndexOf(element);

        public static int IndexIn<T>(this T element, IEnumerable<T> given) => element.IndexIn(given.ToArray());

        public static bool IsUniqueIn<T>(this T element, T[] given)
        {
            bool passed = false;
            bool result = true;
            given.ForEach(e =>
            {
                if (e.Equals(element))
                {
                    if (passed) result = false;
                    passed = true;
                }
            });
            return result;
        }

        public static bool IsUniqueIn<T>(this T element, IEnumerable<T> given) => element.IsUniqueIn(given.ToArray());

        public static Counter GetCounter<T>(this T[] given) => new(0, given.Length, 1, 0);

        public static void AddTo<T>(this T given, List<T> list) => list.Add(given);

        public static void RemoveFrom<T>(this T given, List<T> list) => list.Remove(given);

        public static void AddAllTo<T>(this T[] given, List<T> list) => list.AddRange(given);

        public static void AddAllTo<T>(this IEnumerable<T> given, List<T> list) => list.AddRange(given);

        public static bool TrueForAll<T>(this T[] given, Predicate<T> predicate) => Array.TrueForAll(given, predicate);

        public static bool TrueForAll<T>(this IEnumerable<T> given, Predicate<T> predicate) => given.ToArray().TrueForAll(predicate);

        public static bool TrueForAllIndices<T>(this T[] given, Predicate<int> predicate)
        {
            for (int a = 0; a < given.Length; a++) if (!predicate(a)) return false;
            return true;
        }

        public static bool TrueForAllIndices<T>(this List<T> given, Predicate<int> predicate) => given.ToArray().TrueForAllIndices(predicate);

        public static void ForFirstThatSatisfies<T>(this T[] given, Predicate<T> predicate, Action<T> action)
        {
            foreach (T element in given)
            {
                if (predicate(element))
                {
                    action(element);
                    break;
                }
            }
        }

        public static void ForFirstThatSatisfies<T>(this IEnumerable<T> given, Predicate<T> predicate, Action<T> action) =>
            given.ToArray().ForFirstThatSatisfies(predicate, action);

        public static void ForLastThatSatisfies<T>(this T[] given, Predicate<T> predicate, Action<T> action)
        {
            int index = -1;
            for (int a = 0; a < given.Length; a++) if (predicate(given[a])) index = a;
            if (index >= 0) action(given[index]);
        }

        public static void ForLastThatSatisfies<T>(this IEnumerable<T> given, Predicate<T> predicate, Action<T> action) =>
            given.ToArray().ForLastThatSatisfies(predicate, action);

        public static void ForEachThatSatisfies<T>(this T[] given, Predicate<T> predicate, Action<T> action) =>
            given.ForEach(element => element.DoIf(predicate, action));

        public static void ForEachThatSatisfies<T>(this IEnumerable<T> given, Predicate<T> predicate, Action<T> action) =>
            given.ToArray().ForEachThatSatisfies(predicate, action);

        public static void DoIfAllSatisfy<T>(this T[] given, Predicate<T> predicate, Action action) =>
            given.TrueForAll(predicate).DoIf(v => v, v => action());

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        public static T? Find<T>(this T[] array, Predicate<T> predicate) => Array.Find(array, predicate);

#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        public static T[] FindAll<T>(this T[] array, Predicate<T> predicate) => Array.FindAll(array, predicate);

        public static T[] ToArray<T>() => Enum.GetValues(typeof(T)).Cast<T>().ToArray();

        public static void Add<T>(this List<T> list, params T[] elements) => elements.ForEach(e => list.Add(e));

        public static List<T> ItemsOfType<T>(this ArrayList given)
        {
            List<T> result = new();
            foreach (object item in given) if (item is T t) result.Add(t);
            return result;
        }

        public static bool HasSameValues<T, U>(this Dictionary<T, U> given, Dictionary<T, U> other)
        {
            foreach (KeyValuePair<T, U> pair in given)
            {
                if (!other.ContainsKey(pair.Key)) return false;
                if (!given[pair.Key].Equals(other[pair.Key])) return false;
            }

            return true;
        }

        public static int GetRandomIndex<T>(this IEnumerable<T> given) => new Random().Next(given.ToList().Count);

        public static T GetRandomElement<T>(this IEnumerable<T> given) => given.ToList()[given.GetRandomIndex()];

        public static T[] GetRandomElements<T>(this IEnumerable<T> given, int number, bool uniqueConstraint = false)
        {
            List<T> copy = given.ToList();
            List<T> result = new();
            for (int a = 0; a < number; a++)
            {
                int index = copy.GetRandomIndex();
                result.Add(copy[index]);
                if (uniqueConstraint)
                    copy.RemoveAt(index);
            }
            return result.ToArray();
        }

        public static void Switch<T>(this T[] given, int a, int b)
        {
            T aCopy = given[a], bCopy = given[b];
            (given[a], given[b]) = (bCopy, aCopy);
        }

        public static int IndexOf<T>(this IEnumerable<T> given, T element) => given.ToList().IndexOf(element);

        public static T[] Rotated<T>(this IEnumerable<T> given, int degree)
        {
            T[] array = given.ToArray();
            List<int> indexes = new();
            while (degree <= array.Length) degree += array.Length;
            for (int a = 0; a < array.Length; (a, degree) = (a + 1, degree + 1))
            {
                while (degree >= array.Length) degree -= array.Length;
                indexes.Add(degree);
            }
            List<T> result = new();
            indexes.ForEach(index => result.Add(array[index]));
            return result.ToArray();
        }

        public static T[] Without<T>(this IEnumerable<T> given, int index)
        {
            List<T> result = new();
            for (int a = 0; a < given.Count(); a++)
            {
                if (a == index) continue;
                result.Add(given.ToList()[a]);
            }
            return result.ToArray();
        }

        public static void Add<T>(this List<T> given, T element, out T added)
        {
            added = element;
            given.Add(element);
        }

        public static bool Remove<T>(this List<T> given, T element, out T removed)
        {
            removed = element;
            return given.Remove(element);
        }

        public static Dictionary<U, T> Flipped<T, U>(this Dictionary<T, U> given)
        {
            Dictionary<U, T> result = new();
            foreach (KeyValuePair<T, U> pair in given)
                result.Add(pair.Value, pair.Key);
            return result;
        }

        public static U[] Mapped<T, U>(this IEnumerable<T> given, Func<T, U> func)
        {
            List<U> result = new();
            given.ForEach(t => result.Add(func(t)));
            return result.ToArray();
        }

        public static IEnumerable<T> GetEnumerable<T>(this IEnumerable<T> input, int startingIndex)
        {
            List<T> list = input.ToList();
            List<T> result = new List<T>();
            int currentIndex = startingIndex;
            for (int a = 0; a < list.Count; a++)
            {
                if (currentIndex == list.Count) currentIndex = 0;
                result.Add(list[currentIndex]);
                currentIndex++;
            }
            return result;
        }

        public static int IndexOf<T>(this IEnumerable<T> list, int startingIndex, T item) => list.GetEnumerable(startingIndex).IndexOf(item);

        public static IEnumerable<T> GetValues<T>() => Enum.GetValues(typeof(T)).Cast<T>();

        public static IEnumerable<R> ProductOf<F, S, R>(IEnumerable<F> first, IEnumerable<S> second, Func<F, S, R> conversion)
        {
            List<R> result = new();
            foreach (F fElement in first)
                foreach (S sElement in second)
                    result.Add(conversion(fElement, sElement));
            return result;
        }

        public static void ForEach<T>(this IEnumerable source, Action<T> action) =>
            source.ForEach<T>(element => element.DoIf(e => e is T, e => action(e)));

        public static T[][] SeparateEachIntoColumns<T>(this IEnumerable<T> given, int columns)
        {
            List<T>[] separated = new List<T>[columns];
            for (int a = 0; a < separated.Length; a++) separated[a] = new();
            for (int a = 0; a < given.ToList().Count; a++)
            {
                int index = a;
                while (index >= separated.ToList().Count) index -= separated.ToList().Count;
                separated[index].Add(given.ToList()[a]);
            }
            return separated.Mapped(original => original.ToArray());
        }

        public static T[] Spliced<T>(this T[] given, int start, int end, int step = 1)
        {
            T[] spliced = given[start..end];
            List<T> result = new();
            for (int index = start; index < spliced.Length; index += step)
                result.Add(spliced[index]);
            return result.ToArray();
        }
    }
}