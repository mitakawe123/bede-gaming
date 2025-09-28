using SimplifiedLotteryGame.DTOs;
using SimplifiedLotteryGame.Models;
using SimplifiedLotteryGame.Models.Players;

namespace SimplifiedLotteryGame.Tests.HouseTests;

public class HouseTests
{
    private static Player CreateHumanPlayer(uint? ticketsCount = null)
    {
        var player = new HumanPlayer();
        player.BuyTickets(ticketsCount);
        return player;
    }
    
    private static Player CreateCpuPlayer(uint? ticketsCount = null)
    {
        var player = new CpuPlayer();
        player.BuyTickets(ticketsCount);
        return player;
    }

    [Fact]
    public void CalculateRevenue_ShouldSumBalancesAndTickets()
    {
        // Arrange
        var players = new List<Player>
        {
            CreateHumanPlayer(2),
            CreateCpuPlayer(3)
        };

        var house = new House();

        // Act
        house.CalculateRevenue(players);

        // Assert
        Assert.Equal(20m, house.Revenue);
    }

    [Fact]
    public void RecordWinnings_ShouldReduceRevenueAndIncreasePlayerBalance()
    {
        // Arrange
        var player = CreateHumanPlayer(2);
        var ticket = player.Tickets.First();

        var house = new House();
        house.CalculateRevenue(new List<Player> { player });
        var initialRevenue = house.Revenue;

        var results = new List<WinningResult>
        {
            new(player, ticket, 5m) // player wins $5
        };

        // Act
        house.RecordWinnings(results);

        // Assert
        Assert.Equal(initialRevenue - 5m, house.Revenue);
        Assert.Equal(13m, player.Balance); // (initialBalance - tickets cost) + winning amount
    }

    [Fact]
    public void RecordWinnings_ShouldHandleMultipleResults()
    {
        // Arrange
        var player1 = CreateCpuPlayer(1);
        var player2 = CreateHumanPlayer(5);

        var house = new House();
        house.CalculateRevenue(new List<Player> { player1, player2 });
        var initialRevenue = house.Revenue;

        var results = new List<WinningResult>
        {
            new(player1, player1.Tickets.First(), 3m),
            new(player2, player2.Tickets.First(), 7m)
        };

        // Act
        house.RecordWinnings(results);

        // Assert
        Assert.Equal(initialRevenue - 10m, house.Revenue);
        Assert.Equal(12m, player1.Balance); // (initialBalance - tickets cost) + winning amount
        Assert.Equal(12m, player2.Balance);
    }
}
