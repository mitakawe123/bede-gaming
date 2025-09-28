using SimplifiedLotteryGame.DTOs;
using SimplifiedLotteryGame.Models.Players;

namespace SimplifiedLotteryGame.Models;

public class House
{
    public decimal Revenue { get; private set; }

    public void CalculateRevenue(IReadOnlyCollection<IPlayer> players)
    {
        Revenue = players.Sum(p => p.Balance + p.Tickets.Sum(t => Ticket.Price));
    }

    public void RecordWinnings(IReadOnlyCollection<WinningResult> results)
    {
        foreach (var result in results)
        {
            result.Player.AddWinning(result.Amount);
            Revenue -= result.Amount;
        }
    }
}