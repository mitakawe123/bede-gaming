namespace SimplifiedLotteryGame.Models.Prizes;

public class GrandPrize(decimal percentage, string name) : Prize(percentage, name)
{
    public override void DistributeWinnings()
    {
        throw new NotImplementedException();
    }
}