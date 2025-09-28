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
        House.Print(res);
        
        Console.WriteLine("See you again in Bede Lottery 🎰");
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
}