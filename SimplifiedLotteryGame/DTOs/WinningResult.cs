using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame.DTOs;

public record WinningResult(
    Player Player,
    Ticket Ticket,
    decimal Amount);