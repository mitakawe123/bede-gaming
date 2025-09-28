using SimplifiedLotteryGame.DTOs;

namespace SimplifiedLotteryGame.Models.Prizes;

public abstract class Prize(decimal percentage, string name, double ticketPoolPercentage = 0)
{
    public decimal Percentage { get; } = percentage;

    public string Name { get; } = name;

    protected double TicketPoolPercentage { get; } = ticketPoolPercentage;

    public abstract IReadOnlyCollection<WinningResult> DistributeWinnings(
        List<Ticket> availableTickets, 
        IReadOnlyDictionary<uint, Player> ticketOwners,
        decimal revenue);
}
