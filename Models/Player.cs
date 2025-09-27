namespace SimplifiedLotteryGame.Models;

public sealed class Player
{
    private static uint _nextPlayerId = 1;

    private readonly uint _id;

    public string Name => $"Player {_id}";

    public decimal Balance { get; private set; } = 10; // Each player begins with a starting balance of $10

    public ICollection<Ticket> Tickets { get; } = [];

    public Player()
    {
        _id = _nextPlayerId++;
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

        Print(count);
    }

    private void Print(uint? count)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("🎟️  ");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{Name}");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($" bought {count} ticket{(count > 1 ? "s" : "")}");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(" 💸✨");
        Console.ResetColor();
    }
}