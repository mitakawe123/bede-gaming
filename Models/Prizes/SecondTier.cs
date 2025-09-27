namespace SimplifiedLotteryGame.Models.Prizes;

public class SecondTier(decimal percentage, string name) : Prize(percentage, name)
{
    public override void DistributeWinnings()
    {
        throw new NotImplementedException();
    }
}