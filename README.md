
# Scrappy

A tiny CLI TF2 scrap calculator.

# Build

```sh
dotnet publish -c Release -r linux-x64
dotnet publish -c Release -r win-x64
```

# How to use

```sh
scrappy help        # show help text

scrappy 0.66r =     # show cost in items
# Out: 0 ref. 2 rec. 0 scr.

scrappy 0.66r x 123 # show multiplied cost in items
# Out: 81 ref. 0 rec. 1.5 scr.
```
