using System;

namespace Cards
{
    public class CardNumberBasis : IEquatable<CardNumberBasis>
    {
        public int Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King;

        public CardNumberBasis(int ace = 1, int two = 2, int three = 3, int four = 4, int five = 5, int six = 6, int seven = 7, int eight = 8, int nine = 9,
            int ten = 10, int jack = 11, int queen = 12, int king = 13) => (Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King) =
            (ace, two, three, four, five, six, seven, eight, nine, ten, jack, queen, king);

        public int this[CardNumber number] => number switch
        {
            CardNumber.Ace => Ace,
            CardNumber.Two => Two,
            CardNumber.Three => Three,
            CardNumber.Four => Four,
            CardNumber.Five => Five,
            CardNumber.Six => Six,
            CardNumber.Seven => Seven,
            CardNumber.Eight => Eight,
            CardNumber.Nine => Nine,
            CardNumber.Ten => Ten,
            CardNumber.Jack => Jack,
            CardNumber.Queen => Queen,
            CardNumber.King => King,
            _ => throw new NotImplementedException()
        };

        public int this[DeckCard card] => this[card.Number];

        public bool Equals(CardNumberBasis other) => Ace == other.Ace && Two == other.Two && Three == other.Three && Four == other.Four
            && Five == other.Five && Six == other.Six && Seven == other.Seven && Eight == other.Eight && Nine == other.Nine && Ten == other.Ten && Jack == other.Jack
            && Queen == other.Queen && King == other.King;

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"{Ace}, {Two}, {Three}, {Four}, {Five}, {Six}, {Seven}, {Eight}, {Nine}, {Ten}, {Jack}, {Queen}, {King}";

        public static bool operator ==(CardNumberBasis a, CardNumberBasis b) => a.Equals(b);

        public static bool operator !=(CardNumberBasis a, CardNumberBasis b) => !(a == b);
    }
}