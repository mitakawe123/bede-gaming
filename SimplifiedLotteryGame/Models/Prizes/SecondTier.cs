using SimplifiedLotteryGame.DTOs;

namespace SimplifiedLotteryGame.Models.Prizes;

public class SecondTier() : Prize(0.3m, "Second Tier")
{
    public override IReadOnlyCollection<WinningResult> DistributeWinnings(
        List<Ticket> availableTickets,
        IReadOnlyDictionary<uint, Player> ticketOwners,
        int initialTicketsCount)
    {
        var winnersCount = (int)Math.Round(initialTicketsCount * 0.1);
        var random = new Random();
        var winningTickets = availableTickets
            .OrderBy(p => random.Next())
            .Take(winnersCount)
            .ToList();
        
        var winning = (House.Revenue * Percentage) / winningTickets.Count; // equal win for each ticket
        winningTickets.ForEach(t => availableTickets.Remove(t));

        return [.. winningTickets.Select(winningTicket => new WinningResult(
            Player: ticketOwners[winningTicket.Id], 
            Ticket: winningTicket, 
            Amount: winning))];
    }
}