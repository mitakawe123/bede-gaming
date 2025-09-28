using SimplifiedLotteryGame.DTOs;

namespace SimplifiedLotteryGame.Models.Prizes;

public abstract class Prize(decimal percentage, string name)
{
    public decimal Percentage { get; } = percentage;

    public string Name { get; } = name;

    public abstract IReadOnlyCollection<WinningResult> DistributeWinnings(
        List<Ticket> availableTickets,
        IReadOnlyDictionary<uint, Player> ticketOwners,
        int initialTicketsCount);
}
