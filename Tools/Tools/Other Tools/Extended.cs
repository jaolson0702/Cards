using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class Extended<T>
    {
        public Dictionary<string, object> Extensions = new();
        public T Value;
        public Extended(T value) => Value = value;
        public object this[string given] => Extensions[given];
        public override string ToString() => Value.ToString();
    }
}