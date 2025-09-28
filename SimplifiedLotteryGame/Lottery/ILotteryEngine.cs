using SimplifiedLotteryGame.Models.Players;

namespace SimplifiedLotteryGame.Lottery;

public interface ILotteryEngine
{
    void AddPlayer(IPlayer player);
    void Run();
}