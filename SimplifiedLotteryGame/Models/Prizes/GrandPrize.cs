using SimplifiedLotteryGame.DTOs;

namespace SimplifiedLotteryGame.Models.Prizes;

public class GrandPrize() : Prize(0.5m, "Grand Prize")
{
    public override IReadOnlyCollection<WinningResult> DistributeWinnings(
        List<Ticket> availableTickets,
        IReadOnlyDictionary<uint, Player> ticketOwners,
        int initialTicketsCount)
    {
        var random = new Random().Next(availableTickets.Count);
        var winningTicket = availableTickets[random];
        var winning = House.Revenue * Percentage;
        
        availableTickets.Remove(winningTicket);
        return
        [
            new WinningResult(
                Player: ticketOwners[winningTicket.Id],
                Ticket: winningTicket,
                Amount: winning)
        ];
    }
}