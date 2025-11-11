using Junko.Logic;

namespace Junko.Tests;

public class GameRunnerTests
{
    private class FakeCard : ICard
    {
        public int ID { get; } = new Random().Next();
        public string Color { get; set; } = "Red";
        public string Value { get; set; } = "1";
        public void Play(GameRunner game, Player player) { }
    }

    [Fact]
    public void PlayerTests()
    {
        var game = new GameRunner(7);
        bool result = game.CreatePlayer("Jason");
        Assert.True(result);
        Assert.Single(game.Players);
        Assert.Equal("Jason", game.Players[0].Name);
    }
    [Fact]
    public void DealCards()
    {
        var game = new GameRunner(7);
        game.CreatePlayer("Jason");
        game.CreatePlayer("Damian");
        var deck = new List<ICard>();
        for (int i = 0; i < deck.Count; i++)
        {
            deck.Add(new FakeCard { Color = "Red", Value = i.ToString() });
        }
        bool started = game.Start(deck);
        Assert.True(started);
        Assert.True(game.Running);
        Assert.All(game.Players, p => Assert.Equal(7, p.Hand.Count));
        //Assert.Equal((deck.Count - 7) * game.Players.Count, deck.Count);
    }
}
