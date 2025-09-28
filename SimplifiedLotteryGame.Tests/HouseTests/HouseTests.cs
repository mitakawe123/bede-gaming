using System.Reflection;
using SimplifiedLotteryGame.DTOs;
using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame.Tests.HouseTests;

public class HouseTests
{
    private static Player CreatePlayer(uint? ticketsCount = null)
    {
        var player = new Player();
        player.BuyTickets(ticketsCount);
        return player;
    }

    private static void ResetRevenue()
    {
        var property = typeof(House).GetProperty(nameof(House.Revenue), BindingFlags.Static | BindingFlags.Public)!;
        property.SetValue(null, 0m);
    }

    [Fact]
    public void CalculateRevenue_ShouldSumBalancesAndTickets()
    {
        ResetRevenue();
        
        // Arrange
        var players = new List<Player>
        {
            CreatePlayer(2),
            CreatePlayer(3)
        };

        // Act
        House.CalculateRevenue(players);

        // Assert
        Assert.Equal(20m, House.Revenue);
    }

    [Fact]
    public void RecordWinnings_ShouldReduceRevenueAndIncreasePlayerBalance()
    {
        ResetRevenue();
        
        // Arrange
        var player = CreatePlayer(2);
        var ticket = player.Tickets.First();

        House.CalculateRevenue([player]);
        var initialRevenue = House.Revenue;

        var results = new List<WinningResult>
        {
            new(player, ticket, 5m) // player wins $5
        };

        // Act
        House.RecordWinnings(results);

        // Assert
        Assert.Equal(initialRevenue - 5m, House.Revenue);
        Assert.Equal(13m, player.Balance); // (initialBalance - tickets cost) + winning amount
    }

    [Fact]
    public void RecordWinnings_ShouldHandleMultipleResults()
    {
        ResetRevenue();
        
        // Arrange
        var player1 = CreatePlayer(1);
        var player2 = CreatePlayer(5);

        House.CalculateRevenue([player1, player2]);
        var initialRevenue = House.Revenue;

        var results = new List<WinningResult>
        {
            new(player1, player1.Tickets.First(), 3m),
            new(player2, player2.Tickets.First(), 7m)
        };

        // Act
        House.RecordWinnings(results);

        // Assert
        Assert.Equal(initialRevenue - 10m, House.Revenue);
        Assert.Equal(12m, player1.Balance); // (initialBalance - tickets cost) + winning amount
        Assert.Equal(12m, player2.Balance);
    }
}