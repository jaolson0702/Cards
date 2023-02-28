using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cards;
using Tools;

namespace _27CardTrickWPF
{
    /// <summary>
    /// Interaction logic for ColsWindow.xaml
    /// </summary>
    public partial class ColsWindow : Window
    {
        private List<Card> cards;
        private Placement[] placements;
        private int targetNumber;
        private int counter = 0;

        public ColsWindow(List<Card> cards, int targetNumber)
        {
            this.cards = cards;
            this.targetNumber = targetNumber;
            this.placements = Process.GetPlacements(this.targetNumber);
            InitializeComponent();
            Update();
        }

        public void Update()
        {
            Card[][] separated = cards.SeparateEachIntoColumns(3);
            col1ListBox.ItemsSource = separated[0].ToList();
            col2ListBox.ItemsSource = separated[1].ToList();
            col3ListBox.ItemsSource = separated[2].ToList();
            col1Button.IsEnabled = true;
            col2Button.IsEnabled = true;
            col3Button.IsEnabled = true;
        }

        public void Proceed(int selection, Placement placement)
        {
            Card[][] separated = cards.SeparateEachIntoColumns(3);
            cards = new();
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
            groups.ForEach(group => cards.AddRange(group));
            cards.Reverse();
            counter++;
            if (counter == 3)
            {
                MessageBox.Show($"Your card is the {cards[targetNumber - 1]}.".ToUpper());
                Close();
            }
            else Update();
        }

        private void col1Button_Click(object sender, RoutedEventArgs e) => Proceed(1, placements[counter]);

        private void col2Button_Click(object sender, RoutedEventArgs e) => Proceed(2, placements[counter]);

        private void col3Button_Click(object sender, RoutedEventArgs e) => Proceed(3, placements[counter]);
    }
}