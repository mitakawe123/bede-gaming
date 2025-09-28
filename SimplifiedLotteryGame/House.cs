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

    public static void Print(IReadOnlyCollection<WinningResult> results)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n✨🎉 Winning Tickets 🎉✨\n");
        Console.ResetColor();

        foreach (var (player, ticket, amount) in results)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("🎟️ Ticket ID: ");
            Console.ResetColor();
            Console.Write($"{ticket.Id}  ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"💰 {player.Name} Winnings: ");
            Console.ResetColor();
            Console.Write($"{amount:C} ");
            Console.WriteLine($"Balance: {player.Balance:C}");
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n🏆 Good luck to all players! 🏆\n");
        Console.ResetColor();

        Console.WriteLine("-----------------------------------");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("🏦 House Profit: ");
        Console.ResetColor();
        Console.WriteLine($"{Revenue:C}");

        Console.WriteLine("-----------------------------------\n");
    }
}