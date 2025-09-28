using SimplifiedLotteryGame.Models;
using SimplifiedLotteryGame.Models.Players;

namespace SimplifiedLotteryGame.DTOs;

public record WinningResult(
    IPlayer Player,
    Ticket Ticket,
    decimal Amount);