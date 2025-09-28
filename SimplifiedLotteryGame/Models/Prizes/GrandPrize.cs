using SimplifiedLotteryGame.DTOs;

namespace SimplifiedLotteryGame.Models.Prizes;

public class GrandPrize() : Prize(0.5m, "Grand Prize")
{
    public override IReadOnlyCollection<WinningResult> DistributeWinnings(
        List<Ticket> availableTickets, 
        IReadOnlyDictionary<uint, Player> ticketOwners,
        decimal revenue)
    {
        var random = new Random().Next(availableTickets.Count);
        var ticket = availableTickets[random];
        var amount = revenue * Percentage;
        
        availableTickets.Remove(ticket);
        return
        [
            new WinningResult(
                Player: ticketOwners[ticket.Id],
                Ticket: ticket,
                Amount: amount)
        ];
    }
}