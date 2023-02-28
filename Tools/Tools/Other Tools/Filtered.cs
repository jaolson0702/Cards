using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class Filtered<T>
    {
        private Predicate<T> filter;
        private T value;
        public Action OnViolation = () => throw new ArgumentException("The given value does not fulfill the specified filter.");

        public virtual Predicate<T> Filter
        {
            get => filter;
            set
            {
                if (value is not null && !value(Value)) OnViolation?.Invoke();
                else filter = value;
            }
        }

        public virtual T Value
        {
            get => value;
            set
            {
                if (Filter is not null && !Filter(value)) OnViolation?.Invoke();
                else this.value = value;
            }
        }

        public Filtered(T value, Predicate<T> filter = null) => (this.filter, Value) = (filter, value);

        public Filtered(Predicate<T> filter) => this.filter = filter;

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => Value.ToString();

        public static bool operator ==(Filtered<T> a, Filtered<T> b) => a.Value.Equals(b.Value);

        public static bool operator !=(Filtered<T> a, Filtered<T> b) => !(a == b);

        public static explicit operator T(Filtered<T> value) => value.Value;
    }
}