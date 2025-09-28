namespace SimplifiedLotteryGame.Models.Players;

public class CpuPlayer : Player
{
    private static readonly Random Rng = new();
    
    public override void BuyTickets(uint? count = null)
    {
        var ticketsToBuy = count ?? (uint)Rng.Next(MinimumTicketCount, MaximumTicketCount + 1);

        for (var i = 0; i < ticketsToBuy; i++)
        {
            var ticket = new Ticket(Id);
            Tickets.Add(ticket);
            Balance -= Ticket.Price;
        }
    }
}