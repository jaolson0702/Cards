using System.Collections.Generic;

namespace Cards
{
    public class CardSuitComparer : IComparer<DeckCard>
    {
        public CardSuitBasis SuitBasis;
        public CardNumberBasis NumberBasis;

        public CardSuitComparer(CardSuitBasis suitBasis, CardNumberBasis numberBasis) => (SuitBasis, NumberBasis) = (suitBasis, numberBasis);

        public CardSuitComparer() : this(new(), new())
        {
        }

        public int Compare(DeckCard a, DeckCard b)
        {
            int suitResult = a.Value(SuitBasis).CompareTo(b.Value(SuitBasis));
            return suitResult == 0 ? a.Value(NumberBasis).CompareTo(b.Value(NumberBasis)) : suitResult;
        }
    }
}