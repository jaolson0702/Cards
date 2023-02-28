using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class FuncTools
    {
        public static void Invoke(this Counter counter, Action action, bool increments = true, bool allowWithoutLimit = false)
        {
            if (!allowWithoutLimit)
            {
                if (increments && counter.UpperLimit is null) throw new NoLimitException("The given parameters indicate that an upper limit must be specified for the counter to invoke the given action.");
                if (!increments && counter.LowerLimit is null) throw new NoLimitException("The given parameters indicate than a lower limit must be specified for the counter to invoke the given action.");
            }
            counter.EnsureActivity();
            while (counter.IsActive)
            {
                action();
                if (increments) counter.Increment();
                else counter.Decrement();
            }
        }
        public static void Invoke(this Action action, Counter counter, bool increments = true, bool allowWithoutLimit = false) =>
            counter.Invoke(action, increments, allowWithoutLimit);
        public static void Invoke(this Action action, uint num) { for (int a = 0; a < num; a++) action(); }
        public static void Invoke(this Action<uint> action, uint num) { for (int a = 0; a < num; a++) action(num); }
        public static void Invoke(this Action<int> action, uint num) { for (int a = 0; a < num; a++) action((int)num); }
        public static bool Satisfies<T>(this T given, Predicate<T> predicate) => predicate(given);
        public static bool Satisfies<T>(this T given, params Predicate<T>[] predicates) => predicates.TrueForAll(predicate => predicate(given));
        public static bool Satisfies<T>(this T given, IEnumerable<Predicate<T>> predicates) => given.Satisfies(predicates.ToArray());
        public static int Compare<T>(this Func<T, T, int> comparer, T a, T b) => comparer(a, b);
        public static bool DoIf<T>(this T given, Predicate<T> predicate, Action<T> action)
        {
            if (predicate(given))
            {
                action(given);
                return true;
            }
            return false;
        }
        public static bool DoIf<T>(this Action<T> action, Predicate<T> predicate, T given)
        {
            if (predicate(given)) { action(given); return true; }
            return false;
        }
        public static bool DoIf<T>(this T given, Predicate<T> predicate, Action<T> affirmative, Action<T> negative)
        {
            bool result = true;
            if (!given.DoIf(predicate, affirmative)) { negative(given); result = false; }
            return result;
        }
        public static bool DoAllIf<T>(this T given, Predicate<T> predicate, Action<T>[] actions)
        {
            if (predicate(given)) { actions.ForEach(a => a(given)); return true; }
            return false;
        }
        public static void DoWhenSatisfied<T>(this T given, Predicate<T> predicate, Action<T>[] actions) =>
            actions.ForEach(a => given.DoIf(predicate, a));

        public static T Trail<T>(this Func<T, T> start, T parameter, params Func<T, T>[] actions)
        {
            parameter = start(parameter);
            foreach (Func<T, T> action in actions.Without(0)) parameter = action(parameter);
            return parameter;
        }
    }
}