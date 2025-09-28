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
        var exception = Record.Exception(() => lotteryGame.StartGame());
        Assert.Null(exception); // should not throw
    }

    [Fact]
    public void StartGame_ShouldThrow_WhenHumanInputIsInvalid()
    {
        // Arrange
        var lotteryGame = new LotteryGame();
        using var stringReader = new StringReader("invalid_number");
        Console.SetIn(stringReader);
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => lotteryGame.StartGame());
    }
}
