namespace SimplifiedLotteryGame.Tests.LotteryGameTests;

public class LotteryGameTests
{
    [Fact]
    public void StartGame_ShouldRunFullFlow_WithCpuAndHumanPlayers()
    {
        // Arrange
        var lotteryGame = new LotteryGame();

        using var stringReader = new StringReader("3");
        Console.SetIn(stringReader);

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        lotteryGame.StartGame();

        // Assert: check console output contains expected messages
        var output = stringWriter.ToString();
        Assert.Contains("Welcome to the Bede Lottery", output);
        Assert.Contains("See you again in Bede Lottery", output);
        Assert.Contains("bought 3 ticket", output);

        var winningTickets = House.GetWinners();
        Assert.NotEmpty(winningTickets); // at least one winning ticket should exist

        var houseRevenue = House.GetRevenue();
        Assert.True(houseRevenue >= 0);
    }
}
