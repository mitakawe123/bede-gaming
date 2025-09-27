using SimplifiedLotteryGame.Models;
using SimplifiedLotteryGame.Models.Prizes;

namespace SimplifiedLotteryGame.Tests.PrizeTests;

public class PrizeTests
{
    private readonly List<Ticket> _initialTickets = GenerateTickets(new Random().Next(100, 200));

    private static int _initialTicketsCount = 0;

    private static List<Ticket> GenerateTickets(int count)
    {
        var tickets = new List<Ticket>();
        for (int i = 0; i < count; i++)
        {
            tickets.Add(new Ticket());
        }

        _initialTicketsCount = tickets.Count;
        return tickets;
    }

    [Fact]
    public void GrandPrize_Distribute_ToOneWinner()
    {
        // Arrange
        var prize = new GrandPrize();

        // Act
        prize.DistributeWinnings(_initialTickets, _initialTicketsCount);

        // Assert
        Assert.Equal(_initialTicketsCount - 1, _initialTickets.Count);
    }

    [Fact]
    public void SecondTierPrize_Distribute_ToMultipleWinners()
    {
        // Arrange
        var prize = new SecondTier();

        // Act
        prize.DistributeWinnings(_initialTickets, _initialTicketsCount);

        // Assert
        Assert.True(_initialTickets.Count <= _initialTicketsCount - 10); // at least 0.1 from the minimum tickets are removed
    }
    
    [Fact]
    public void ThirdTierPrize_Distribute_ToMultipleWinners()
    {
        // Arrange
        var prize = new ThirdTier();

        // Act
        prize.DistributeWinnings(_initialTickets, _initialTicketsCount);

        // Assert
        Assert.True(_initialTickets.Count <= _initialTicketsCount - 20); // at least 0.2 from the minimum tickets are removed
    }
}

