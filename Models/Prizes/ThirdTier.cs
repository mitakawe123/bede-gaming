namespace SimplifiedLotteryGame.Models.Prizes;

public class ThirdTier() : Prize(0.1m, "Third Tier")
{
    public override void DistributeWinnings(List<Ticket> availableTickets, int initialTicketsCount)
    {
        var winnersCount = (int)Math.Round(initialTicketsCount * 0.2);
        var random = new Random();
        var randomWinningTickets = availableTickets
            .OrderBy(p => random.Next())
            .Take(winnersCount)
            .ToList();

        House.AwardWinningTickets(randomWinningTickets, Percentage);
        randomWinningTickets.ForEach(t => availableTickets.Remove(t));
    }
}