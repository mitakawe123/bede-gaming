using SimplifiedLotteryGame.Models;
using SimplifiedLotteryGame.Models.Players;

namespace SimplifiedLotteryGame.Tests.PlayerTests;

public class BuyTicketsTests
{
    [Fact]
    public void BuyTickets_WithFixedCount()
    {
        // Arrange
        var player = new HumanPlayer();
        var startingBalance = player.Balance;
        const uint ticketsToBuy = 3;

        // Act
        player.BuyTickets(ticketsToBuy);

        // Arrange
        var ticketsCost = player.Tickets.Count * Ticket.Price;
        Assert.Equal((int)ticketsToBuy, player.Tickets.Count);
        Assert.Equal(startingBalance - ticketsCost, player.Balance);
    }

    [Fact]
    public void BuyTickets_WithRandomCount()
    {
        // Arrange
        var player = new CpuPlayer();

        // Act
        player.BuyTickets();

        // Arrange
        Assert.InRange(player.Tickets.Count, Player.MinimumTicketCount, Player.MaximumTicketCount);
    }
}