using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Cards
{
    public static class CardTools
    {
        public static Card[] CardsWith(this Card[] cards, CardColor color)
        {
            List<Card> result = new();
            cards.ForEachThatSatisfies(card => card.Color == color, result.Add);
            return result.ToArray();
        }

        public static DeckCard[] CardsWith(this Card[] cards, CardSuit suit)
        {
            List<DeckCard> result = new();
            cards.ForEach(card => card.DoIf(c => c is DeckCard && (c as DeckCard).Suit == suit, c => result.Add(c as DeckCard)));
            return result.ToArray();
        }

        public static DeckCard[] CardsWith(this Card[] cards, CardNumber number)
        {
            List<DeckCard> result = new();
            cards.ForEach(card => card.DoIf(c => c is DeckCard && (c as DeckCard).Number == number, c => result.Add(c as DeckCard)));
            return result.ToArray();
        }

        public static IEnumerable<Card> CardsWith(this IEnumerable<Card> cards, CardColor color) => cards.ToArray().CardsWith(color);

        public static IEnumerable<DeckCard> CardsWith(this IEnumerable<Card> cards, CardSuit suit) => cards.ToArray().CardsWith(suit);

        public static IEnumerable<DeckCard> CardsWith(this IEnumerable<Card> cards, CardNumber number) => cards.ToArray().CardsWith(number);
    }
}