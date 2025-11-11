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
}
