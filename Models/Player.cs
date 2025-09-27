namespace SimplifiedLotteryGame.Models;

public sealed class Player
{
    private static uint _nextPlayerId = 1;
    
    public uint Id { get; }

    public string Name => $"Player {Id}";

    public decimal Balance { get; private set; } = 10; // Each player begins with a starting balance of $10

    public ICollection<Ticket> Tickets { get; } = [];

    public Player()
    {
        Id = _nextPlayerId++;
    }

    public void BuyTickets(uint? count = null)
    {
        if (count is null)
        {
            var randomTicketsNumber = new Random().Next(1, 10); // All players (human and CPU) are limited to purchasing between 1 and 10 tickets
            count = (uint)randomTicketsNumber;
        }
        
        if (count is < 1 or > 10)
            throw new ArgumentException("Tickets must be between 1 and 10");

        for (var i = 0; i < count; i++)
        {
            var ticket = new Ticket();
            Tickets.Add(ticket);
            Balance -= Ticket.Price;
        }

        Console.WriteLine($"{Name} has bought {count} tickets");
    }
}