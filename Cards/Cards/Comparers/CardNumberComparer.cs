using System.Collections.Generic;

namespace Cards
{
    public class CardNumberComparer : IComparer<DeckCard>
    {
        public CardNumberBasis NumberBasis;
        public CardSuitBasis SuitBasis;

        public CardNumberComparer(CardNumberBasis numberBasis, CardSuitBasis suitBasis) => (NumberBasis, SuitBasis) = (numberBasis, suitBasis);

        public CardNumberComparer() : this(new(), new())
        {
        }

        public int Compare(DeckCard a, DeckCard b)
        {
            int numberResult = a.Value(NumberBasis).CompareTo(b.Value(NumberBasis));
            return numberResult == 0 ? a.Value(SuitBasis).CompareTo(b.Value(SuitBasis)) : numberResult;
        }
    }
}