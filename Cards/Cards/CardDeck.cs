using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Cards
{
    public class CardDeck
    {
        public List<Card> Cards { get; private set; }

        public CardDeck(bool includeJokers = false, bool shuffle = true) => Reset(includeJokers, shuffle);

        public Card[] this[CardColor color] => Cards.CardsWith(color).ToArray();
        public DeckCard[] this[CardSuit suit] => Cards.CardsWith(suit).ToArray();
        public DeckCard[] this[CardNumber number] => Cards.CardsWith(number).ToArray();
        public Card this[int index] => Cards[index];
        public int Count => Cards.Count;

        public T[] Get<T>() where T : Card
        {
            List<T> result = new();
            Cards.ForEach(card => card.DoIf(c => c is T, c => result.Add(c as T)));
            return result.ToArray();
        }

        public void Reset(bool includeJokers = false, bool shuffle = true)
        {
            Cards = new();
            CollectionTools.ToArray<CardSuit>().ForEach(suit =>
                CollectionTools.ToArray<CardNumber>().ForEach(number => Cards.Add((number, suit))));
            if (includeJokers) Cards.Add(Card.Get(CardColor.Black), Card.Get(CardColor.Red));
            if (shuffle) Shuffle();
        }

        public void Shuffle() => Cards = Cards.Scrambled();
    }
}