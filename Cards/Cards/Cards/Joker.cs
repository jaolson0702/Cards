using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Joker : Card
    {
        private static readonly Dictionary<CardColor, Joker> jokers = new();
        public override CardColor Color { get; }

        private Joker(CardColor color) => Color = color;

        public override bool Equals(Card other) => other is Joker j && Color == j.Color;

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"{Color} Joker";

        public new static Joker Get(CardColor color)
        {
            if (!jokers.ContainsKey(color))
                jokers.Add(color, new Joker(color));
            return jokers[color];
        }

        public static implicit operator Joker(CardColor color) => Get(color);

        public static bool operator ==(Joker a, Joker b) => a.Equals(b);

        public static bool operator !=(Joker a, Joker b) => !(a == b);
    }
}