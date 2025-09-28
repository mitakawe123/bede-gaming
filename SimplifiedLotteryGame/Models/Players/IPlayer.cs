namespace SimplifiedLotteryGame.Models.Players;

public interface IPlayer
{
    uint Id { get; }
    string Name { get; }
    decimal Balance { get; }
    ICollection<Ticket> Tickets { get; }

    void AddWinning(decimal amount);
    void BuyTickets(uint? count = null);
}