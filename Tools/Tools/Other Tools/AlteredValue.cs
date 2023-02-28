using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class AlteredValue<T, U>
    {
        public T Input;
        public Func<T, U> Alteration;

        public AlteredValue(T input, Func<T, U> alteration = null) => (Input, Alteration) = (input, alteration);

        public U Get => Alteration(Input);

        public override string ToString() => Get.ToString();
    }
}