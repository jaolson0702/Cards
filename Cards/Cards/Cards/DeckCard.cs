using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Tools;

namespace Cards
{
    public enum CardNumber
    { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }

    public enum CardSuit
    { Clubs, Diamonds, Hearts, Spades }

    public class DeckCard : Card
    {
        private static readonly Dictionary<Tuple<CardNumber, CardSuit>, DeckCard> deckCards = new();
        public readonly CardNumber Number;
        public readonly CardSuit Suit;

        private DeckCard(CardNumber number, CardSuit suit) => (Number, Suit) = (number, suit);

        public override CardColor Color => Suit == CardSuit.Clubs || Suit == CardSuit.Spades ? CardColor.Black : CardColor.Red;

        public int Value(CardNumberBasis basis) => basis[Number];

        public int Value(CardSuitBasis basis) => basis[Suit];

        public override bool Equals(Card other) => other is DeckCard dc && Number == dc.Number && Suit == dc.Suit;

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"{Number} of {Suit}";

        public new static DeckCard Get(CardNumber number, CardSuit suit)
        {
            bool matchingTupleFound = false;
            foreach (Tuple<CardNumber, CardSuit> tuple in deckCards.Keys)
                if (tuple.Item1 == number && tuple.Item2 == suit) matchingTupleFound = true;
            if (!matchingTupleFound)
                deckCards.Add(new(number, suit), new(number, suit));
            return deckCards[Array.Find(deckCards.Keys.ToArray(), key => key.Item1 == number && key.Item2 == suit)];
        }

        public static implicit operator DeckCard(ValueTuple<CardNumber, CardSuit> values) => Get(values.Item1, values.Item2);

        public static implicit operator DeckCard(ValueTuple<CardSuit, CardNumber> values) => Get(values.Item2, values.Item1);

        public static bool operator ==(DeckCard a, DeckCard b) => a.Equals(b);

        public static bool operator !=(DeckCard a, DeckCard b) => !(a == b);
    }
}