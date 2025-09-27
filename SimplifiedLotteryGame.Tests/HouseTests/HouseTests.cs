using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame.Tests.HouseTests;

public class HouseTests
{
    private static List<Player> GeneratePlayersWithTickets(int playerCount, uint? ticketsPerPlayer)
    {
        var players = new List<Player>();
        for (int i = 0; i < playerCount; i++)
        {
            var player = new Player();
            player.BuyTickets(ticketsPerPlayer);
            players.Add(player);
        }
        return players;
    }

    [Fact]
    public void CalculateRevenue_ShouldSumPlayerBalancesAndTicketPrices()
    {
        // Arrange
        var players = GeneratePlayersWithTickets(2, 3);

        // Act
        House.CalculateRevenue(players);
        var expectedRevenue = players.Sum(p => p.Balance + p.Tickets.Count * Ticket.Price);

        // Assert
        Assert.Equal(expectedRevenue, House.GetRevenue());
    }

    [Fact]
    public void AwardWinningTicket_ShouldAssignCorrectWinningAmount()
    {
        // Arrange
        var players = GeneratePlayersWithTickets(1, 2);
        House.CalculateRevenue(players);
        var ticket = players[0].Tickets.First();
        var prizePercentage = 0.5m;
        var expectedWinning = House.GetRevenue() * prizePercentage;

        // Act
        House.AwardWinningTicket(ticket, prizePercentage);

        // Assert
        var winners = House.GetWinners();
        Assert.True(winners.ContainsKey(ticket.Id));
        Assert.Equal(expectedWinning, winners[ticket.Id]);
        Assert.Equal(House.GetRevenue(), (players.Sum(p => p.Balance + p.Tickets.Count * Ticket.Price) - expectedWinning));
    }

    [Fact]
    public void AwardWinningTickets_ShouldDistributeWinningEqually()
    {
        // Arrange
        var players = GeneratePlayersWithTickets(1, 3);
        House.CalculateRevenue(players);
        var tickets = players[0].Tickets.ToList();
        const decimal prizePercentage = 0.6m;
        var expectedEachWinning = (House.GetRevenue() * prizePercentage) / tickets.Count;

        // Act
        House.AwardWinningTickets(tickets, prizePercentage);

        // Assert
        var winners = House.GetWinners();
        foreach (var ticket in tickets)
        {
            Assert.True(winners.ContainsKey(ticket.Id));
            Assert.Equal(expectedEachWinning, winners[ticket.Id]);
        }

        var totalWinnings = expectedEachWinning * tickets.Count;
        Assert.Equal(House.GetRevenue(), (players.Sum(p => p.Balance + p.Tickets.Count * Ticket.Price) - totalWinnings));
    }
}
