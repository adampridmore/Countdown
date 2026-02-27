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

## Deployment (Render)

This application is fully containerized using Docker and is ready to be hosted as a **Web Service** on [Render.com](https://render.com)'s free tier!

1. Commit and push your code to a GitHub repository.
2. Sign in to Render and click **New > Web Service**.
3. Connect your GitHub repository.
4. Render will automatically detect the `Dockerfile`.
5. Name your service (e.g. `countdown-solver`) and click **Create Web Service**.

*Note: The free tier spins down after 15 minutes of inactivity, so the very first loading screen after an idle period may take ~30 seconds, but otherwise calculations will be instant!*
