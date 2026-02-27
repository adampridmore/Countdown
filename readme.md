# Countdown Solver

## Letters

```bash
dotnet run -l bucteasdn
```

## Numbers

```bash
dotnet run -n "25 8 6 9 7 2" 682
```

Or in release mode (which is a lot quicker with optimizations)

```bash
dotnet run -c Release -n "25 8 6 9 7 2" 682
```

Or with timing
```bash
time dotnet run -c Release -n "25 8 6 9 7 2" 682
```
