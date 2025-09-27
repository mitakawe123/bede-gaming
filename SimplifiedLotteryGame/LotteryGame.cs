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
        
        AddHumanPlayer();
        AddCpuPlayers();
        
        House.CalculateRevenue(_players);
        
        var availableTickets = _players.SelectMany(p => p.Tickets).ToList();
        var initialTicketsCount = availableTickets.Count;
        
        _prizes.ForEach(prize => prize.DistributeWinnings(availableTickets, initialTicketsCount));

        House.Print();
        
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
        Console.WriteLine($"How many tickets do you want to buy, {humanPlayer.Name}?");
        if (uint.TryParse(Console.ReadLine(), out var ticketNumber))
        {
            humanPlayer.BuyTickets(ticketNumber);
            _players.Add(humanPlayer);
        }
        else
        {
            throw new ArgumentException("Please enter a valid number");
        }
    }
}