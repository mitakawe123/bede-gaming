using SimplifiedLotteryGame.Models;
using SimplifiedLotteryGame.Models.Players;
using SimplifiedLotteryGame.Models.Prizes;

namespace SimplifiedLotteryGame.Tests.PrizeTests;

public class PrizeTests
{
    private static List<IPlayer> GeneratePlayers(int count, int seed = 42)
    {
        var random = new Random(seed);
        var players = new List<IPlayer>();

        for (int i = 0; i < count; i++)
        {
            var player = new HumanPlayer();
            player.BuyTickets((uint)random.Next(Player.MinimumTicketCount, Player.MaximumTicketCount));
            players.Add(player);
        }

        return players;
    }

    private static Dictionary<uint, IPlayer> BuildTicketOwners(List<IPlayer> players) =>
        players.SelectMany(p => p.Tickets, (p, t) => new { p, t })
               .ToDictionary(x => x.t.Id, x => x.p);

    [Fact]
    public void GrandPrize_ShouldRemoveExactlyOneTicket()
    {
        // Arrange
        var players = GeneratePlayers(15);
        var tickets = players.SelectMany(p => p.Tickets).ToList();
        var owners = BuildTicketOwners(players);

        var house = new House();
        house.CalculateRevenue(players);

        var prize = new GrandPrize();
        
        // Act
        prize.DistributeWinnings(tickets, owners, house.Revenue);

        // Assert
        Assert.Equal(owners.Count - 1, tickets.Count);
    }

    [Fact]
    public void SecondTier_ShouldRemoveAtLeastTenPercent()
    {
        // Arrange
        var players = GeneratePlayers(15);
        var tickets = players.SelectMany(p => p.Tickets).ToList();
        var owners = BuildTicketOwners(players);

        var house = new House();
        house.CalculateRevenue(players);

        var prize = new SecondTier();
        
        // Act
        prize.DistributeWinnings(tickets, owners, house.Revenue);

        // Assert
        var removed = owners.Count - tickets.Count;
        Assert.True(removed >= (int)Math.Round(owners.Count * 0.1));
    }

    [Fact]
    public void ThirdTier_ShouldRemoveAtLeastTwentyPercent()
    {
        // Arrange
        var players = GeneratePlayers(15);
        var tickets = players.SelectMany(p => p.Tickets).ToList();
        var owners = BuildTicketOwners(players);

        var house = new House();
        house.CalculateRevenue(players);

        var prize = new ThirdTier();
        
        // Act
        prize.DistributeWinnings(tickets, owners, house.Revenue);

        // Assert
        var removed = owners.Count - tickets.Count;
        Assert.True(removed >= (int)Math.Round(owners.Count * 0.2));
    }
}
