namespace SimplifiedLotteryGame.Models;

public sealed class Ticket
{
    private static uint _nextTicketId = 1;
    
    public uint Id { get; }

    public static uint Price => 1; // Tickets are priced at $1 each

    public Ticket()
    {
        Id = _nextTicketId++;
    }
}