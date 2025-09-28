namespace SimplifiedLotteryGame.Models;

public sealed class Player
{
    private static uint _nextPlayerId = 1;

    public uint Id { get; }

    public const int MinimumTicketCount = 1;
    
    public const int MaximumTicketCount = 10;

    public string Name => $"Player {Id}";

    public decimal Balance { get; private set; } = 10; // Each player begins with a starting balance of $10

    public ICollection<Ticket> Tickets { get; } = [];

    public Player()
    {
        Id = _nextPlayerId++;
    }
    
    public void AddWinning(decimal amount) => Balance += amount;

    public void BuyTickets(uint? count = null)
    {
        if (count is null)
        {
            var randomTicketsNumber = new Random().Next(MinimumTicketCount, MaximumTicketCount); // All players (human and CPU) are limited to purchasing between 1 and 10 tickets
            count = (uint)randomTicketsNumber;
        }

        for (var i = 0; i < count; i++)
        {
            var ticket = new Ticket(Id);
            Tickets.Add(ticket);
            Balance -= Ticket.Price;
        }
    }
}