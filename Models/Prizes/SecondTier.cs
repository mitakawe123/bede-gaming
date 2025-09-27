namespace SimplifiedLotteryGame.Models.Prizes;

public class SecondTier() : Prize(0.3m, "Second Tier")
{
    public override void DistributeWinnings(List<Ticket> availableTickets, int initialTicketsCount)
    {
        var winnersCount = (int)Math.Round(initialTicketsCount * 0.1);
        var random = new Random();
        var randomWinningTickets = availableTickets
            .OrderBy(p => random.Next())
            .Take(winnersCount)
            .ToList();

        House.AwardWinningTickets(randomWinningTickets, Percentage);
        randomWinningTickets.ForEach(t => availableTickets.Remove(t));
    }
}