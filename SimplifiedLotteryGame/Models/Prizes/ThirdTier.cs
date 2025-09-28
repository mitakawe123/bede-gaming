using SimplifiedLotteryGame.DTOs;

namespace SimplifiedLotteryGame.Models.Prizes;

public class ThirdTier() : Prize(0.1m, "Third Tier")
{
    public override IReadOnlyCollection<WinningResult> DistributeWinnings(
        List<Ticket> availableTickets,
        IReadOnlyDictionary<uint, Player> ticketOwners,
        int initialTicketsCount)
    {
        var winnersCount = (int)Math.Round(initialTicketsCount * 0.2);
        var random = new Random();
        var winningTickets = availableTickets
            .OrderBy(p => random.Next())
            .Take(winnersCount)
            .ToList();

        winningTickets.ForEach(t => availableTickets.Remove(t));
        
        var winning = (House.Revenue * Percentage) / winningTickets.Count; // equal win for each ticket
        winningTickets.ForEach(t => availableTickets.Remove(t));

        return [.. winningTickets.Select(winningTicket => new WinningResult(
            Player: ticketOwners[winningTicket.Id], 
            Ticket: winningTicket, 
            Amount: winning))];
    }
}