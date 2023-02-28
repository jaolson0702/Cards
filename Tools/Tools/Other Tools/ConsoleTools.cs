using System;
using System.Collections.Generic;

namespace Tools
{
    public static class ConsoleTools
    {
        public static void Write<T>(this T var, string before = "", string after = "") => Console.Write($"{before}{var}{after}");

        public static void WriteLine<T>(this T var, string before = "", string after = "") => Console.WriteLine($"{before}{var}{after}");

        public static int Read<T>(this T message)
        {
            message.Write();
            return Console.Read();
        }

        public static string ReadLine<T>(this T message)
        {
            message.Write();
            return Console.ReadLine();
        }

        public static void ClearWrite<T>(this T var, string before = "", string after = "")
        {
            Console.Clear();
            var.Write(before, after);
        }

        public static void ClearWriteLine<T>(this T var, string before = "", string after = "")
        {
            Console.Clear();
            var.WriteLine(before, after);
        }

        public static int ClearRead<T>(this T message)
        {
            Console.Clear();
            return message.Read();
        }

        public static string ClearReadLine<T>(this T message)
        {
            Console.Clear();
            return message.ReadLine();
        }

        public static int ReadValidate<T>(this T message, Predicate<int> condition, Func<int, int> actionWhileValidating = null, Func<int, int> actionIfInvalid = null, Func<int, int> actionIfValid = null, bool repeatUntilValid = false)
        {
            int result = message.Read();
            if (actionWhileValidating is not null)
                result = actionWhileValidating(result);
            if (!condition(result) && actionIfInvalid is not null)
                result = actionIfInvalid(result);
            if (condition(result) && actionIfValid is not null)
                result = actionIfValid(result);
            if (!condition(result) && repeatUntilValid)
                message.ReadValidate(condition, actionWhileValidating, actionIfInvalid, actionIfValid, repeatUntilValid);
            return result;
        }

        public static string ReadLineValidate<T>(this T message, Predicate<string> condition, Func<string, string> actionWhileValidating = null, Func<string, string> actionIfInvalid = null, Func<string, string> actionIfValid = null, bool repeatUntilValid = false)
        {
            string result = message.ReadLine();
            if (actionWhileValidating is not null)
                result = actionWhileValidating(result);
            if (!condition(result) && actionIfInvalid is not null)
                result = actionIfInvalid(result);
            if (condition(result) && actionIfValid is not null)
                result = actionIfValid(result);
            if (!condition(result) && repeatUntilValid)
                message.ReadLineValidate(condition, actionWhileValidating, actionIfInvalid, actionIfValid, repeatUntilValid);
            return result;
        }
    }
}