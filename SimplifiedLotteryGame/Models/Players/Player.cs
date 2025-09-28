namespace SimplifiedLotteryGame.Models.Players;

public abstract class Player : IPlayer
{
    private static uint _nextPlayerId = 1;

    public uint Id { get; }
    
    public string Name => $"Player {Id}";
    
    public decimal Balance { get; protected set; } = 10; // Starting balance
    
    public ICollection<Ticket> Tickets { get; } = [];

    public const int MinimumTicketCount = 1;
    
    public const int MaximumTicketCount = 10;

    protected Player()
    {
        Id = _nextPlayerId++;
    }

    public void AddWinning(decimal amount) => Balance += amount;

    public abstract void BuyTickets(uint? count = null);
}