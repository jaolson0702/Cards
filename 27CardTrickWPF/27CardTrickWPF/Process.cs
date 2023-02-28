using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;
using Tools;

namespace _27CardTrickWPF
{
    public class Process
    {
        public static Placement[] GetPlacements(int number)
        {
            List<Placement> result = new();
            int sumOfDigits = number.ToString().ToCharArray().Mapped(c => c.ToString().ToInt()).Sum();
            switch (sumOfDigits)
            {
                case 1:
                    result.Add(Placement.Bottom);
                    result.Add(Placement.Bottom);
                    break;

                case 2:
                    result.Add(Placement.Middle);
                    result.Add(Placement.Bottom);
                    break;

                case 3:
                    result.Add(Placement.Top);
                    result.Add(Placement.Bottom);
                    break;

                case 4:
                    result.Add(Placement.Bottom);
                    result.Add(Placement.Middle);
                    break;

                case 5:
                    result.Add(Placement.Middle);
                    result.Add(Placement.Middle);
                    break;

                case 6:
                    result.Add(Placement.Top);
                    result.Add(Placement.Middle);
                    break;

                case 7:
                    result.Add(Placement.Bottom);
                    result.Add(Placement.Top);
                    break;

                case 8:
                    result.Add(Placement.Middle);
                    result.Add(Placement.Top);
                    break;

                case 9:
                    result.Add(Placement.Top);
                    result.Add(Placement.Top);
                    break;
            }

            if (number <= 9) result.Add(Placement.Bottom);
            else if (number <= 18) result.Add(Placement.Middle);
            else if (number <= 27) result.Add(Placement.Top);
            return result.ToArray();
        }
    }
}