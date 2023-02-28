using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public struct Substring : IComparable<Substring>, IEquatable<Substring>
    {
        public int Min, Max;
        public Substring(int min, int max) => (Min, Max) = (min, max);
        public int CompareTo(Substring other)
        {
            if (Min != other.Min) return Min.CompareTo(other.Min);
            return Max.CompareTo(other.Max);
        }
        public bool Equals(Substring other) => CompareTo(other) == 0;
        public string Get(string given) => given.Substring(Min, Max);
        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => $"Substring({Min}, {Max})";
        public static bool operator ==(Substring left, Substring right) => left.Equals(right);
        public static bool operator !=(Substring left, Substring right) => !(left == right);
    }
}