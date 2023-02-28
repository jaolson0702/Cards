using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace _27CardTrick
{
    internal class Process
    {
        public static void Run()
        {
            Console.Clear();
            List<Card> cards = new CardDeck().Cards.GetRandomElements(27, true).ToList();
            int number = new Random().Next(1, 28);
            Placement[] placements = GetPlacements(number);
            cards.ToString("\n").WriteLine("\n");
            "\nHere are 27 random cards from the deck. Your job is to memorize one of them.".WriteLine();
            "\nType ENTER when you have a card memorized and are ready to move on: ".ReadLine();
            "\nThe cards have been dealt into three groups, as shown below:".ClearWriteLine();
            cards = Round(cards, placements[0]);
            "\nGood. Now they have been dealt into three different groups. Let's do this again.".ClearWriteLine();
            cards = Round(cards, placements[1]);
            "\nGreat! One more time:".ClearWriteLine();
            cards = Round(cards, placements[2]);
            $"\n\n\nYour card is the {cards[number - 1]}.".ToUpper().ClearWriteLine(after: "\n\n\n");
        }

        public static List<Card> Round(List<Card> given, Placement placement)
        {
            Card[][] separated = given.SeparateEachIntoColumns(3);
            int selection = 0;
            separated.ForEachIndex(index =>
                $"\nGroup {index + 1}:\n\n{separated[index].ToString("\n")}".WriteLine());
            selection = "\nWhich group is your card in? Type 1, 2, or 3: ".ReadLineValidate((input) => int.TryParse(input, out int _) && input.ToInt() > 0 && input.ToInt() <= separated.Length, (input) => input = input.Trim(), (input) => { "1, 2, or 3 must be entered.".WriteLine(); return input; }, repeatUntilValid: true).ToInt();
            List<Card> result = new();
            List<List<Card>> groups = separated.Without(selection - 1).Mapped(group => group.ToList().Reversed()).ToList();
            switch (placement)
            {
                case Placement.Bottom:
                    groups.Add(separated[selection - 1].ToList().Reversed());
                    break;

                case Placement.Middle:
                    groups.Insert(1, separated[selection - 1].ToList().Reversed());
                    break;

                case Placement.Top:
                    groups.Insert(0, separated[selection - 1].ToList().Reversed());
                    break;
            }
            groups.ForEach(group => result.AddRange(group));
            return result.Reversed();
        }

        public static Placement[] GetPlacements(int number)
        {
            List<Placement> result = new();
            int sumOfDigits = number.ToString().ToCharArray().Mapped(c => c.ToString().ToInt()).Sum();
            switch (sumOfDigits)
            {
                case 1:
                    result.Add(Placement.Bottom);
                    result.Add(Placement.Bottom);
                    break;

                case 2:
                    result.Add(Placement.Middle);
                    result.Add(Placement.Bottom);
                    break;

                case 3:
                    result.Add(Placement.Top);
                    result.Add(Placement.Bottom);
                    break;

                case 4:
                    result.Add(Placement.Bottom);
                    result.Add(Placement.Middle);
                    break;

                case 5:
                    result.Add(Placement.Middle);
                    result.Add(Placement.Middle);
                    break;

                case 6:
                    result.Add(Placement.Top);
                    result.Add(Placement.Middle);
                    break;

                case 7:
                    result.Add(Placement.Bottom);
                    result.Add(Placement.Top);
                    break;

                case 8:
                    result.Add(Placement.Middle);
                    result.Add(Placement.Top);
                    break;

                case 9:
                    result.Add(Placement.Top);
                    result.Add(Placement.Top);
                    break;
            }

            if (number <= 9) result.Add(Placement.Bottom);
            else if (number <= 18) result.Add(Placement.Middle);
            else if (number <= 27) result.Add(Placement.Top);
            return result.ToArray();
        }
    }
}