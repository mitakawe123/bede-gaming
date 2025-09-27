using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame;

public static class House
{
    private static decimal _revenue;

    private static readonly Dictionary<uint, decimal> WinningTickets = [];
    
    public static decimal GetRevenue() => _revenue;
    
    public static IReadOnlyDictionary<uint, decimal> GetWinners() => WinningTickets;
    
    public static void CalculateRevenue(IReadOnlyCollection<Player> players)
    {
        _revenue = players.Sum(p => p.Balance + p.Tickets.Sum(t => Ticket.Price));
    }

    public static void AwardWinningTicket(Ticket winningTicket, decimal prizePercentage)
    {
        var winning = _revenue * prizePercentage;
        WinningTickets[winningTicket.Id] = winning;
        _revenue -= winning;
    }

    public static void AwardWinningTickets(IReadOnlyCollection<Ticket> winningTickets, decimal prizePercentage)
    {
        var ticketWinning = (_revenue * prizePercentage) / winningTickets.Count; // equal win for each ticket
        foreach (var winningTicket in winningTickets)
        {
            WinningTickets[winningTicket.Id] = ticketWinning;
            _revenue -= ticketWinning;
        }
    }

    public static void Print()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n✨🎉 Winning Tickets 🎉✨\n");
        Console.ResetColor();

        foreach (var (ticketId, winningAmount) in WinningTickets)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("🎟️ Ticket ID: ");
            Console.ResetColor();
            Console.Write($"{ticketId}  ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("💰 Winnings: ");
            Console.ResetColor();
            Console.WriteLine($"{winningAmount:C}");
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n🏆 Good luck to all players! 🏆\n");
        Console.ResetColor();

        Console.WriteLine("-----------------------------------");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("🏦 House Profit: ");
        Console.ResetColor();
        Console.WriteLine($"{_revenue:C}");

        Console.WriteLine("-----------------------------------\n");
    }
}