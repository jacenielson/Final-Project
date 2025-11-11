namespace Junko.Logic;

public interface ICard
{
    int ID { get; }
    CardColor Color { get; }
    CardType Type { get; }
    string Value { get; }
    void Play(GameRunner game, Player player);
}
public enum CardColor
{
    Red,
    Blue,
    Green,
    Yellow,
    Wild
}
public enum CardType
{
    Number,
    Skip,
    Reverse,
    DrawTwo,
    Wild,
    WildDrawFour
}