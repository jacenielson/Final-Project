namespace Junko.Logic;
public class Card : ICard
{
    private static int nextId = 0;

    public int ID { get; }
    public string Name { get; }
    public CardColor Color { get; }
    public CardType Type { get; }
    public string Value { get; }

    public Card(CardColor color, string value)
    {
        ID = nextId;
        Color = color;
        Value = value;
        Type = CardType.Number;
        Name = $"{color} {value}";
    }
    public void Play(GameRunner game, Player player)
    {
        game.Stack.Add(this);
    }
    public override string ToString() => Name;
}