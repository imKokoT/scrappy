
const string VERSION = "1.0";
const string YEARS = "2026";
const string AUTHOR = "imKokoT";

if (args.Length == 0)
{
    Console.WriteLine($"Scrappy V{VERSION} Copyright {AUTHOR} ({YEARS})\n\n" +
                      "A tiny CLI TF2 scrap calculator; see 'help' subcommand to get help information");
    return 0;
}

// commands
try
{
    switch (args)
    {
        case ["help"]:
            Console.WriteLine("Currency explanation:\n" +
                              "- s -> scrap\n" +
                              "- c -> rec. metal\n" +
                              "- r -> ref. metal");
            break;

        // get price in items 1.66r = 1r 2q
        case [var price, "=", ..var options]:
            Console.WriteLine(GetPriceInItems(price));
            break;

        default:
            Console.WriteLine("error: invalid arguments");
            return 1;
    }
}
catch (Exception e) when (e is FormatException or ArgumentException)
{
    Console.WriteLine($"format error: {e.Message}");
    return 1;
}

return 0;

// ======================================================

static string GetPriceInItems(string price)
{
    char currency = price.Last();
    double value = double.Parse(price[..^1]);

    int scrapValue = (int)Math.Round(GetScrapValue(value, currency));

    int r = scrapValue / 9;
    int c = scrapValue % 9 / 3;
    int s = scrapValue % 3;

    return $"{r} ref. {c} rec. {s} scr.";
}

static double GetScrapValue(double value, char currency)
{
    return currency switch
    {
        's' => value,
        'c' => value * 3,
        'r' => value * 9,
        _ => throw new ArgumentException($"invalid currency '{currency}'")
    };
}
