namespace Junko.Logic;
public class Player
{
    public string Name { get; private set; }
    public List<ICard> Hand;

    public Player(string name)
    {
        Name = name;
        Hand = new List<ICard>();
    }
}