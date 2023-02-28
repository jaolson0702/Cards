using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cards;
using Tools;

namespace Hearts
{
    public class HeartsSet
    {
        private readonly HeartsGame game;
        private bool heartsBroken = false;

        public HeartsSet(HeartsGame game)
        {
            this.game = game;
            DealCards();
            StartSet();
            game.Players.ForEachIndex(index =>
            {
                if (game.Players[index].Points == 26)
                {
                    game.Players[index].Points = 0;
                    game.Players.Without(index).ForEach(player => player.Points = 26);
                    $"\n{game.Players[index].Name.ToUpper()} SHOT THE MOON\n".WriteLine();
                }
            });
        }

        private void RunSet(HeartsPlayer startingPlayer, DeckCard startingCard)
        {
            if (startingPlayer.Name != "You")
            {
                "\n".ClearWriteLine();
                game.Players.ForEach(player => $"{player.Name}: {player.Points} points".WriteLine());
            }
            "\n".WriteLine();
            HeartsPlayer[] order = game.Players.Rotated(startingPlayer.IndexIn(game.Players));
            startingPlayer.Remove(startingCard);
            startingPlayer.Name.Join(startingCard, " - ").WriteLine();
            List<DeckCard> cards = new() { startingCard };
            DeckCard maxCard = startingCard;
            foreach (HeartsPlayer player in order.Without(0))
            {
                Thread.Sleep(500);
                DeckCard chosen = null;
                if (player.Name == "You")
                {
                    do
                    {
                        DeckCard[] availableCards = player.GetAvailableCards(startingCard.Suit).Sorted(new CardSuitComparer(game.CardSuitBasis, game.CardNumberBasis)).ToArray();
                        availableCards.ToString("\nYour Available Cards:\n", ".\t", "\n", "", "").WriteLine();
                        bool isValid = true;
                        int number = 0;
                        do
                        {
                            isValid = true;
                            do
                            {
                                string input = "\nChoose the number of the card you would like to put down: ".ReadLine();
                                isValid = int.TryParse(input, out number);
                            } while (!isValid);
                            if (number > availableCards.Length) isValid = false;
                        } while (!isValid);
                        chosen = availableCards[number - 1];
                    } while (chosen is null);
                    Console.WriteLine();
                }
                else
                    chosen = player.GetAvailableCards(startingCard.Suit).GetRandomElement();
                player.Remove(chosen);
                cards.Add(chosen);
                player.Name.Join(chosen, " - ").WriteLine();
                if (!heartsBroken && chosen.IsPointCard())
                {
                    Thread.Sleep(500);
                    heartsBroken = true;
                    "HEARTS HAVE BEEN BROKEN".WriteLine();
                    Thread.Sleep(500);
                }
                if (chosen.Suit == startingCard.Suit && chosen.Value(game.CardNumberBasis) > maxCard.Value(game.CardNumberBasis))
                    maxCard = chosen;
            }
            Thread.Sleep(600);
            HeartsPlayer maxPlayer = order[cards.IndexOf(maxCard)];
            int previousPoints = maxPlayer.Points;
            cards.ForEach(card => maxPlayer.Points += card.Points());
            $"\n\n{maxPlayer.Name} get{(maxPlayer.Name == "You" ? "" : "s")} {maxPlayer.Points - previousPoints} point{(maxPlayer.Points - previousPoints == 1 ? "" : "s")}\n".WriteLine();
            "\nPress ENTER to continue: ".ReadLine();
            if (maxPlayer.Cards.Count > 0)
            {
                DeckCard[] availableCards = maxPlayer.GetAvailableCards(heartsBroken).Sorted(new CardSuitComparer(game.CardSuitBasis, game.CardNumberBasis)).ToArray();
                DeckCard chosen = null;
                if (maxPlayer.Name == "You")
                {
                    Console.Clear();
                    game.Players.ForEach(player => $"{player.Name}: {player.Points} points".WriteLine());
                    do
                    {
                        availableCards.ToString("\nYour Available Cards:\n", ".\t", "\n", "", "").WriteLine();
                        bool isValid;
                        int number = 0;
                        do
                        {
                            isValid = true;
                            do
                            {
                                string input = "\nChoose the number of the card you would like to put down: ".ReadLine();
                                isValid = int.TryParse(input, out number);
                            } while (!isValid);
                            if (number > availableCards.Length) isValid = false;
                        } while (!isValid);
                        chosen = availableCards[number - 1];
                    } while (chosen is null);
                }
                else chosen = availableCards.GetRandomElement();
                RunSet(maxPlayer, chosen);
            }
        }

        private void StartSet()
        {
            foreach (HeartsPlayer player in game.Players)
            {
                DeckCard toc = Card.Get(CardNumber.Two, CardSuit.Clubs);
                if (player.HasCard(toc))
                {
                    if (player.Name == "You")
                    {
                        "\n".ClearWriteLine();
                        game.Players.ForEach(player => $"{player.Name}: {player.Points} points".WriteLine());
                    }
                    RunSet(player, toc);
                    break;
                }
            }
        }

        private void DealCards()
        {
            game.Players.ForEach(player => player.Cards.Clear());
            game.Deck.Shuffle();
            game.Players.ForEachIndex(index =>
            {
                for (int a = 0; a < 13; a++) game.Players[index].Cards.Add(game.Deck[index * 13 + a] as DeckCard);
            });
        }
    }
}