using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Hearts
{
    public class HeartsPlayer
    {
        private readonly HeartsGame game;
        public readonly List<DeckCard> Cards = new();
        public int Points = 0;

        public HeartsPlayer(HeartsGame game) => this.game = game;

        public string Name
        {
            get
            {
                int index = game.Players.IndexOf(this) + 1;
                return index == game.Players.Length ? "You" : $"Player {index}";
            }
        }

        public string Pos => Name == "You" ? "Your" : $"{Name}'s";

        public bool HasCard(DeckCard given)
        {
            foreach (DeckCard card in Cards)
                if (card.Number == given.Number && card.Suit == given.Suit)
                    return true;
            return false;
        }

        public DeckCard[] GetAvailableCards(bool heartsBroken)
        {
            List<DeckCard> result = new();
            Cards.ForEach(card =>
            {
                if (heartsBroken || !card.IsPointCard())
                    result.Add(card);
            });
            return result.ToArray();
        }

        public DeckCard[] GetAvailableCards(CardSuit suit)
        {
            List<DeckCard> result = new();
            Cards.ForEach(card =>
            {
                if (card.Suit == suit) result.Add(card);
            });
            if (result.Count == 0) return Cards.ToArray();
            return result.ToArray();
        }

        public void Remove(DeckCard given)
        {
            for (int a = 0; a < Cards.Count; a++)
            {
                if (Cards[a].Number == given.Number && Cards[a].Suit == given.Suit)
                {
                    Cards.RemoveAt(a);
                    break;
                }
            }
        }
    }
}