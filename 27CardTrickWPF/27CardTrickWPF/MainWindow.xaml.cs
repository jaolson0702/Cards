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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cards;
using Tools;

namespace _27CardTrickWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Card> cards;
        private ColsWindow colsWindow;

        public MainWindow()
        {
            InitializeComponent();
            Reset();
        }

        private void Reset()
        {
            cards = new CardDeck().Cards.GetRandomElements(27, true).ToList();
            colsWindow = new(cards, new Random().Next(1, 28));
            cardsListBox.ItemsSource = cards;
        }

        private void proceedButton_Click(object sender, RoutedEventArgs e)
        {
            colsWindow.ShowDialog();
            Reset();
        }
    }
}