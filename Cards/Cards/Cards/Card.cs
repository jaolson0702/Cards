using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public enum CardColor
    { Black, Red }

    public abstract class Card : IEquatable<Card>
    {
        public abstract CardColor Color { get; }

        public abstract bool Equals(Card other);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static Joker Get(CardColor color) => Joker.Get(color);

        public static DeckCard Get(CardNumber number, CardSuit suit) => DeckCard.Get(number, suit);

        public static implicit operator Card(ValueTuple<CardNumber, CardSuit> values) => Get(values.Item1, values.Item2);

        public static implicit operator Card(ValueTuple<CardSuit, CardNumber> values) => Get(values.Item2, values.Item1);

        public static implicit operator Card(CardColor color) => (Joker)color;

        public static bool operator ==(Card a, Card b) => a.Equals(b);

        public static bool operator !=(Card a, Card b) => !(a == b);
    }
}