using SimplifiedLotteryGame.DTOs;
using SimplifiedLotteryGame.Models.Players;
using SimplifiedLotteryGame.Models.Prizes;

namespace SimplifiedLotteryGame.Lottery;

public class LotteryGame
{
    private readonly LotteryEngine _engine;
    
    public LotteryGame()
    {
        var prizeTypes = typeof(Prize).Assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Prize)) && !t.IsAbstract)
            .Select(t => (Prize)Activator.CreateInstance(t)!)
            .OrderByDescending(p => p.Percentage);

        _engine = new LotteryEngine(prizeTypes);
    }

    public void Start()
    {
        AddHumanPlayer();
        AddCpuPlayers();
        _engine.Run();
    }

    private void AddHumanPlayer()
    {
        var human = new HumanPlayer();
        uint ticketNumber;

        while (true)
        {
            Console.WriteLine($"How many tickets for {human.Name}? (Between {Player.MinimumTicketCount} and {Player.MaximumTicketCount})");
            var input = Console.ReadLine();

            if (uint.TryParse(input, out ticketNumber) 
                && ticketNumber is >= Player.MinimumTicketCount and <= Player.MaximumTicketCount)
            {
                break; // valid input
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Invalid input. Please enter a number between {Player.MinimumTicketCount} and {Player.MaximumTicketCount}.");
            Console.ResetColor();
        }

        human.BuyTickets(ticketNumber);
        _engine.AddPlayer(human);
    }

    private void AddCpuPlayers()
    {
        var rng = new Random();
        int count = rng.Next(9, 14);
        for (int i = 0; i < count; i++)
        {
            var cpu = new CpuPlayer();
            cpu.BuyTickets();
            _engine.AddPlayer(cpu);
        }
    }
    
    public static void Print(IReadOnlyCollection<WinningResult> results, decimal revenue)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n✨🎉 Winning Tickets 🎉✨\n");
        Console.ResetColor();

        var groupedByPlayer = results
            .GroupBy(r => r.Player.Id);

        foreach (var playerGroup in groupedByPlayer)
        {
            var player = playerGroup.First().Player;
            var totalWinnings = playerGroup.Sum(r => r.Amount);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"💰 {player.Name} Total Winnings: ");
            Console.ResetColor();
            Console.WriteLine($"{totalWinnings:C}  Balance: {player.Balance:C}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("🎟️ Tickets: ");
            Console.ResetColor();

            // List all ticket IDs for this player
            foreach (var result in playerGroup)
            {
                Console.Write($"{result.Ticket.Id} ");
            }
            Console.WriteLine("\n");
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("🏆 Good luck to all players! 🏆\n");
        Console.ResetColor();

        Console.WriteLine("-----------------------------------");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("🏦 House Profit: ");
        Console.ResetColor();
        Console.WriteLine($"{revenue:C}");

        Console.WriteLine("-----------------------------------\n");
        Console.WriteLine("See you again in Bede Lottery 🎰");
    }
}