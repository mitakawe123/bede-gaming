namespace SimplifiedLotteryGame.Models.Prizes;

public class ThirdTier(decimal percentage, string name) : Prize(percentage, name)
{
    public override void DistributeWinnings()
    {
        throw new NotImplementedException();
    }
}