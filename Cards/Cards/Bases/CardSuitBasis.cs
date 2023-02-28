using System;

namespace Cards
{
    public class CardSuitBasis : IEquatable<CardSuitBasis>
    {
        public int Clubs, Diamonds, Hearts, Spades;

        public CardSuitBasis(int clubs = 1, int diamonds = 2, int hearts = 3, int spades = 4) => (Clubs, Diamonds, Hearts, Spades) =
            (clubs, diamonds, hearts, spades);

        public int this[CardSuit suit] => suit switch
        {
            CardSuit.Clubs => Clubs,
            CardSuit.Diamonds => Diamonds,
            CardSuit.Hearts => Hearts,
            CardSuit.Spades => Spades,
            _ => throw new NotImplementedException()
        };

        public int this[DeckCard card] => this[card.Suit];

        public bool Equals(CardSuitBasis other) => Clubs == other.Clubs && Diamonds == other.Diamonds && Hearts == other.Hearts
            && Spades == other.Spades;

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"{Clubs}, {Diamonds}, {Hearts}, {Spades}";

        public static bool operator ==(CardSuitBasis a, CardSuitBasis b) => a.Equals(b);

        public static bool operator !=(CardSuitBasis a, CardSuitBasis b) => !(a == b);
    }
}