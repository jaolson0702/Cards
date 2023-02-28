using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Tools
{
    public static class StopwatchTools
    {
        private static readonly Dictionary<string, Stopwatch> stopwatches = new();
        public static void Start(string given)
        {
            if (!stopwatches.ContainsKey(given)) stopwatches.Add(given, new());
            ValidateInactiveStopwatch(given);
            stopwatches[given].Start();
        }
        public static void Stop(string given)
        {
            if (stopwatches.ContainsKey(given))
                stopwatches[given].Stop();
        }
        public static TimeSpan Elapsed(string given)
        {
            ValidateRunningStopwatch(given);
            return stopwatches[given].Elapsed;
        }

        public static double ElapsedMilliseconds(string given)
        {
            ValidateRunningStopwatch(given);
            return stopwatches[given].ElapsedMilliseconds;
        }
        public static void StartStopwatch(this string given) => Start(given);
        public static void StopStopwatch(this string given) => Stop(given);
        public static TimeSpan ElapsedTime(this string given) => Elapsed(given);
        public static double ElapsedTimeMilliseconds(this string given) => ElapsedMilliseconds(given);
        private static void ValidateStopwatch(string given)
        {
            if (!stopwatches.ContainsKey(given))
                throw new Exception($"The stopwatch with the name \"{given}\" does not exist.");
        }
        private static void ValidateInactiveStopwatch(string given)
        {
            ValidateStopwatch(given);
            if (stopwatches[given].IsRunning)
                throw new Exception($"The stopwatch with the name \"{given}\" is already running.");
        }
        private static void ValidateRunningStopwatch(string given)
        {
            ValidateStopwatch(given);
            if (!stopwatches[given].IsRunning)
                throw new Exception($"The stopwatch with the name \"{given}\" is not running.");
        }
    }
}