namespace SimplifiedLotteryGame.Models;

public sealed class Player
{
    public uint Id { get; } //auto generate this

    public string Name => $"Player {Id}";
    
    public decimal Balance { get; private set; }

    public ICollection<Ticket> Tickets { get; } = [];

    public void BuyTickets(uint count)
    {
        if (Tickets.Count is < 1 or > 10)
            throw new ArgumentException("Tickets must be between 1 and 10");

        for (var i = 0; i < count; i++)
        {
            var ticket = new Ticket();
            Tickets.Add(ticket);
            Balance -= ticket.Price;
        }
    }
}