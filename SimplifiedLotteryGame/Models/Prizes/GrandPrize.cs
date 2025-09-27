namespace SimplifiedLotteryGame.Models.Prizes;

public class GrandPrize() : Prize(0.5m, "Grand Prize")
{
    public override void DistributeWinnings(List<Ticket> availableTickets, int initialTicketsCount)
    {
        var random = new Random().Next(availableTickets.Count);
        var winningTicket = availableTickets[random];
        House.AwardWinningTicket(winningTicket, Percentage);
        availableTickets.Remove(winningTicket);
    }
}