using SimplifiedLotteryGame.Models;
using SimplifiedLotteryGame.Models.Players;
using SimplifiedLotteryGame.Models.Prizes;

namespace SimplifiedLotteryGame.Lottery;

public class LotteryEngine : ILotteryEngine
{
    private readonly List<IPlayer> _players = [];
    private readonly List<Prize> _prizes;

    public LotteryEngine(IEnumerable<Prize> prizes)
    {
        _prizes = prizes.OrderByDescending(p => p.Percentage).ToList();
    }

    public void AddPlayer(IPlayer player) => _players.Add(player);

    public void Run()
    {
        var house = new House();
        house.CalculateRevenue(_players);

        var availableTickets = _players.SelectMany(p => p.Tickets).ToList();
        var ticketOwners = _players
            .SelectMany(p => p.Tickets, (p, t) => new { p, t })
            .ToDictionary(x => x.t.Id, x => x.p);

        var results = _prizes.SelectMany(p => p.DistributeWinnings(availableTickets, ticketOwners, house.Revenue)).ToList();
        house.RecordWinnings(results);

        LotteryGame.Print(results, house.Revenue);
    }
}
