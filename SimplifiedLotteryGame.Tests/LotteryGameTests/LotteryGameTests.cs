using SimplifiedLotteryGame.Lottery;

namespace SimplifiedLotteryGame.Tests.LotteryGameTests;

public class LotteryGameTests
{
    [Fact]
    public void StartGame_ShouldNotThrow_WhenHumanInputIsValid()
    {
        // Arrange
        var lotteryGame = new LotteryGame();
        using var stringReader = new StringReader("1"); // human buys 1 ticket
        Console.SetIn(stringReader);
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act & Assert
        var exception = Record.Exception(() => lotteryGame.Start());
        Assert.Null(exception); // should not throw
    }

    [Fact]
    public void StartGame_ShouldRetry_WhenHumanInputIsInvalid()
    {
        // Arrange
        var lotteryGame = new LotteryGame();

        // Simulate invalid input first, then a valid number (e.g., "3")
        using var stringReader = new StringReader("invalid_number\n3");
        Console.SetIn(stringReader);

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        lotteryGame.Start();

        // Assert
        var output = stringWriter.ToString();
        Assert.Contains("Invalid input", output);
        Assert.Contains("How many tickets", output);
    }
}
