using SimplifiedLotteryGame.DTOs;
using SimplifiedLotteryGame.Models.Players;

namespace SimplifiedLotteryGame.Models.Prizes;

public class SecondTier() : Prize(0.3m, "Second Tier", ticketPoolPercentage: 0.1)
{
    public override IReadOnlyCollection<WinningResult> DistributeWinnings(List<Ticket> availableTickets,
        IReadOnlyDictionary<uint, IPlayer> ticketOwners,
        decimal revenue)
    {
        var winnersCount = (int)Math.Round(ticketOwners.Count * TicketPoolPercentage);
        var random = new Random();
        var winningTickets = availableTickets
            .OrderBy(p => random.Next())
            .Take(winnersCount)
            .ToList();
        
        var amount = (revenue * Percentage) / winningTickets.Count; // equal win for each ticket
        winningTickets.ForEach(t => availableTickets.Remove(t));

        return [.. winningTickets.Select(winningTicket => new WinningResult(
            Player: ticketOwners[winningTicket.Id], 
            Ticket: winningTicket, 
            Amount: amount))];
    }
}