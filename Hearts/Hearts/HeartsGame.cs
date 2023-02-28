using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Hearts
{
    public class HeartsGame : ICardGame
    {
        public CardNumberBasis CardNumberBasis => new(14);
        public CardSuitBasis CardSuitBasis => new();
        public CardDeck Deck { get; } = new(false, false);
        public readonly HeartsPlayer[] Players;

        public HeartsGame()
        {
            Players = new HeartsPlayer[] { new(this), new(this), new(this), new(this) };
            Console.Title = "Hearts";
            while (Players[0].Points < 100 && Players[1].Points < 100 && Players[2].Points < 100 && Players[3].Points < 100)
                _ = new HeartsSet(this);
        }
    }
}