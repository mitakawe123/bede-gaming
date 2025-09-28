using SimplifiedLotteryGame.DTOs;
using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame;

public static class House
{
    public static decimal Revenue { get; private set; }

    public static void CalculateRevenue(IReadOnlyCollection<Player> players)
    {
        Revenue = players.Sum(p => p.Balance + p.Tickets.Sum(t => Ticket.Price));
    }

    public static void RecordWinnings(IReadOnlyCollection<WinningResult> results)
    {
        foreach (var result in results)
        {
            result.Player.AddWinning(result.Amount);
            Revenue -= result.Amount;
        }
    }
}