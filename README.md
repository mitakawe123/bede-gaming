# Bede Lottery Game 🎰

## Overview
Bede Lottery is a simple console-based lottery game implemented in C#.  
Players can be human or CPU, buy lottery tickets, and win prizes based on randomized draws.  
The game is designed to be lightweight, easy to run, and fully playable from the console.

## Features
- Add a human player with custom ticket purchase.
- Add CPU players automatically (9–14 per game).
- Buy multiple tickets per player.
- Randomized prize distribution based on ticket ownership.
- Track house revenue and winnings.
- Console-based UI with friendly messages.

## How to Play
Run the game:

```bash
dotnet run
```

Follow the console instructions:
1. Enter the number of tickets you want to buy.
2. Watch as CPU players buy tickets automatically.
3. Prizes are distributed at the end, and revenue is calculated.
4. The console prints the winners and the final revenue.

<h3>Example</h3>
Welcome to the Bede Lottery 🎰<br>
And hello again Alice<br>
How many tickets do you want to buy, Alice?<br>
3<br>
CPU players have bought their tickets<br>
🏆 Prize winners:<br>
- Alice won $50<br>
- CPU Player 1 won $20<br>
See you again in Bede Lottery 🎰


<h3>Running Tests</h3>

The project includes unit tests for key functionality:
```bash
dotnet test
```

Tests simulate human input and verify that the game flow works as expected.
