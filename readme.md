# Countdown Solver

A modern, fast web application for solving puzzles from the UK TV gameshow "Countdown".
Built with an F# Minimal API backend and a Vanilla JS/CSS glassmorphic frontend.

## How to Run

Start the application using the .NET CLI:

```bash
dotnet run
```

*For maximum performance (highly recommended for the Numbers round), run in Release mode:*
```bash
dotnet run -c Release
```

Once the server starts, open your browser and navigate to the URL provided in the console (e.g. `http://localhost:5000` or `http://localhost:5031`).

## Features

- **Numbers Round**: Solves the numbers game instantly using a highly optimized dynamic programming approach that evaluates valid expressions via bitmask combinations.
- **Letters Round**: Finds the longest valid English words from a set of letters using a recursive pattern matcher against a large 370k word dictionary.
