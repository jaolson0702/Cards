using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tools
{
    public static class ThreadTools
    {
        public static void Wait(TimeSpan timeSpan) => Thread.Sleep(timeSpan);
        public static void Wait(this Action action, TimeSpan timeSpan)
        {
            Wait(timeSpan);
            action();
        }
        public static void Wait(int milliseconds) => Thread.Sleep(milliseconds);
        public static void Wait(this Action action, int milliseconds)
        {
            Wait(milliseconds);
            action();
        }
        public static void WaitSeconds(int seconds) => Wait(seconds * 1000);
        public static void WaitSeconds(this Action action, int seconds) => action.Wait(seconds * 1000);
    }
}