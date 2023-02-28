using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class TemperatureTools
    {
        public static double CF(this double c) => c * (9.ToDouble() / 5) + 32;
        public static double FC(this double f) => (5.ToDouble() / 9) * (f - 32);
        public static double CK(this double c) => c + 273;
        public static double KC(this double k) => k - 273;
        public static double FK(this double f) => f.FC().CK();
    }
}