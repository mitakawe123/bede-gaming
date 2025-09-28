using SimplifiedLotteryGame.DTOs;
using SimplifiedLotteryGame.Models;
using SimplifiedLotteryGame.Models.Prizes;

namespace SimplifiedLotteryGame;

public class LotteryGame
{
    private readonly List<Player> _players = [];
    private readonly List<Prize> _prizes = [];

    public LotteryGame()
    {
        var prizeTypes = typeof(Prize).Assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Prize)) && !t.IsAbstract);

        foreach (var prizeType in prizeTypes)
            if (Activator.CreateInstance(prizeType) is Prize prize)
                _prizes.Add(prize);
        
        _prizes = _prizes.OrderByDescending(p => p.Percentage).ToList();
    }
    
    public void StartGame()
    {
        Console.WriteLine("Welcome to the Bede Lottery 🎰");
        
        AddHumanPlayer(); // These methods can be moved to strategy pattern via (CpuPlayer and HumanPlayer classes) but for a simple console app will be overkill
        AddCpuPlayers();
        
        House.CalculateRevenue(_players);
        
        var availableTickets = _players.SelectMany(p => p.Tickets).ToList();
        var initialTicketsCount = availableTickets.Count;   
        var ticketOwners = _players
            .SelectMany(p => p.Tickets, (p, t) => new { p, t })
            .ToDictionary(x => x.t.Id, x => x.p);
        
        List<WinningResult> res = [.. _prizes.SelectMany(prize => prize.DistributeWinnings(availableTickets, ticketOwners, initialTicketsCount))];
        House.RecordWinnings(res);
        Print(res);
    }

    private void AddCpuPlayers()
    {
        var randomPlayerNumber = new Random().Next(9, 14); // total players = 10–15 (9-14 cpu based and one human)
        for (var i = 0; i < randomPlayerNumber; i++)
        {
            var cpuPlayer = new Player();
            cpuPlayer.BuyTickets();
            _players.Add(cpuPlayer);
        }
    }

    private void AddHumanPlayer()
    {
        var humanPlayer = new Player();
        Console.WriteLine($"And hello again {humanPlayer.Name}");

        uint ticketNumber;
        while (true)
        {
            Console.WriteLine($"How many tickets do you want to buy, {humanPlayer.Name}?");
            var input = Console.ReadLine();

            if (uint.TryParse(input, out ticketNumber) 
                && ticketNumber is >= Player.MinimumTicketCount and <= Player.MaximumTicketCount)
            {
                break; // valid input, exit loop
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Invalid input. Please enter a number between {Player.MinimumTicketCount} and {Player.MaximumTicketCount}.");
            Console.ResetColor();
        }

        humanPlayer.BuyTickets(ticketNumber);
        _players.Add(humanPlayer);
    }
    
    private static void Print(IReadOnlyCollection<WinningResult> results)
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
        Console.WriteLine($"{House.Revenue:C}");

        Console.WriteLine("-----------------------------------\n");
        Console.WriteLine("See you again in Bede Lottery 🎰");
    }
}