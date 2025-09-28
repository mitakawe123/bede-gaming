namespace SimplifiedLotteryGame.Models.Players;

public class HumanPlayer : Player
{
    public override void BuyTickets(uint? count = null)
    {
        var ticketsToBuy = count ?? MinimumTicketCount;

        for (var i = 0; i < ticketsToBuy; i++)
        {
            var ticket = new Ticket(Id);
            Tickets.Add(ticket);
            Balance -= Ticket.Price;
        }
    }
}