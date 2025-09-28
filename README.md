Bede Lottery Game 🎰
Overview

Bede Lottery is a simple console-based lottery game implemented in C#.
Players can be human or CPU, buy lottery tickets, and win prizes based on randomized draws.
The game is designed to be lightweight, easy to run, and fully playable from the console.

Features

Add a human player with custom ticket purchase.

Add CPU players automatically (9–14 per game).

Buy multiple tickets per player.

Randomized prize distribution based on ticket ownership.

Track house revenue and winnings.

Console-based UI with friendly messages.

How to Play

Run the game:

dotnet run


Follow the console instructions:

Enter the number of tickets you want to buy.

Watch as CPU players buy tickets automatically.

Prizes are distributed at the end, and revenue is calculated.

End of the game: The console prints the winners and the final revenue.

Example
Welcome to the Bede Lottery 🎰
And hello again Alice
How many tickets do you want to buy, Alice?
3
CPU players have bought their tickets
🏆 Prize winners:
- Alice won $50
- CPU Player 1 won $20
See you again in Bede Lottery 🎰

Running Tests

The project includes unit tests for key functionality:

dotnet test


Tests simulate human input and verify that the game flow works as expected.

What Can Be Improved

Dependency Injection & Interfaces: Currently the game relies on static House and Console I/O. Using interfaces (IUserInterface, IHouse) allows easier testing and customization.

Player Classes: Currently both CPU and human players use the same Player class. Creating separate HumanPlayer and CpuPlayer classes with a strategy pattern would improve readability and flexibility.

Randomness Control: CPU player count is randomized. For testing, seeding the random number generator would make tests deterministic.

Prize System Extensibility: Adding new prize types requires modifying the assembly and class discovery. A plugin or configuration-based system could simplify this.

UI/UX: A console game could be extended to a GUI or web-based front-end for better user experience.

Error Handling: Input validation is minimal. Handling invalid inputs or unexpected errors more gracefully would improve stability.
