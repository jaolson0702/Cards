using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class MiscTools
    {
        public static double ToDouble(this int given) => Convert.ToDouble(given);

        public static string ToMoney(this decimal given) => $"${given}";

        public static bool Is(this Type given, Type other) => given == other || given.IsSubclassOf(other);

        public static bool? Opposite(this bool? given)
        {
            if (given == null) return given;
            return !((bool)given);
        }
    }
}