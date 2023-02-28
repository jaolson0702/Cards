namespace Cards
{
    public interface ICardGame
    {
        CardNumberBasis CardNumberBasis { get; }
        CardSuitBasis CardSuitBasis { get; }
        CardDeck Deck { get; }
    }
}