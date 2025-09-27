namespace SimplifiedLotteryGame.Models.Prizes;

public abstract class Prize(decimal percentage, string name)
{
    public decimal Percentage { get; } = percentage;

    public string Name { get; } = name;

    public abstract void DistributeWinnings(List<Ticket> availableTickets, int initialTicketsCount);
}
