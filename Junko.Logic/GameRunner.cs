namespace Junko.Logic;

public class GameRunner
{
    private int maxPlayers = 2;
    public List<string> Startinghand;
    public List<ICard> Deck = new List<ICard>();
    public List<ICard> Stack = new List<ICard>();
    public List<Player> Players { get; set; } = new List<Player>();
    public int CurrentPlayer { get; private set; }
    public bool HasDrawnCard { get; private set; }
    bool started = false;
    public bool Running
    {
        get
        {
            return started;
        }
    }
    private int currentturn;
    public int HandCount;
    public int CurrentTurn { get => currentturn; }
    public GameRunner(int handcount, List<string> startinghand) : this(handcount)
    {
        HandCount = handcount;
        Startinghand = startinghand;
    }
    public GameRunner(int handcount)
    {
        Startinghand = new List<string>();
    }
    private static GameRunner? _instance;
    public static GameRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameRunner(7);
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    public event Action? GameStateChanged;
    public bool CreatePlayer(string name)
    {
        if (Players.Count >= maxPlayers)
        {
            return false;
        }
        else
        {
            Players.Add(new Player(name));
            return true;
        }
    }
    public bool Start(List<ICard> Deck)
    {
        foreach (var p in Players)
        {
            p.Hand.AddRange(Deck);
            for (int i = 0; i < 7; i++)
            {
                if (Startinghand.Count > i)
                {
                    var card = Deck.FirstOrDefault(c => c.Value == Startinghand[i]);
                    if (card != null)
                    {
                        p.Hand.Add(card);
                        Deck.Remove(card);
                    }
                    else
                    {
                        DrawCard(p);
                    }
                }
                else
                {
                    DrawCard(p);
                }
            }
        }
        CurrentPlayer = 0;
        HasDrawnCard = false;
        GameStateChanged?.Invoke();
        started = true;
        return true;
    }
    public bool DrawCard(Player p)
    {
        var card = Deck.FirstOrDefault();
        if (card != null)
        {
            p.Hand.Add(card);
            Deck.RemoveAt(0);
        }
        return true;
    }
    public bool DrawCard(int p)
    {
        if (!HasDrawnCard && p == CurrentPlayer)
        {
            HasDrawnCard = DrawCard(Players[p]);
            GameStateChanged?.Invoke();
            return HasDrawnCard;
        }
        return false;
    }
    public bool PlayCard(ICard card)
    {
        var current = Players[CurrentPlayer];
        var TopCard = Stack[Stack.Count - 1];
        if (current.Hand.Contains(card))
        {
            if (card.Value == TopCard.Value || card.Color == TopCard.Color)
            {
                current.Hand.Remove(card);
                Stack.Add(card);
                GameStateChanged?.Invoke();
                return true;
            }
        }
        return false;
    }
}
