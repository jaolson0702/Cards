using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts
{
    public static class HeartsTools
    {
        public static bool IsPointCard(this DeckCard card) => card.Suit == CardSuit.Hearts || card == Card.Get(CardNumber.Queen, CardSuit.Spades);

        public static int Points(this DeckCard card)
        {
            if (card == Card.Get(CardNumber.Queen, CardSuit.Spades)) return 13;
            if (card.IsPointCard()) return 1;
            return 0;
        }
    }
}