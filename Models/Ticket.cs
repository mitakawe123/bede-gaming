namespace SimplifiedLotteryGame.Models;

public sealed class Ticket
{
    public uint Id { get; } //auto generate this

    public uint Price { get; } = 1; // 1 dollar hardcoded for one
}