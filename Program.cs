
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
                              "- w -> weapon\n" +
                              "- s -> scrap\n" +
                              "- c -> rec. metal\n" +
                              "- r -> ref. metal\n" +
                              "\n" +
                              "PRICE FORMAT:\n" +
                              "\t value + currency -> 123.45r\n" +
                              "\n" +
                              "COMMANDS:\n" +
                              "\thelp \t-> show this text\n" +
                              "\tPRICE = \t-> calculate PRICE into separate items; example: '23.83r =' returns '23 ref. 2 rec. 1 scr.'\n" +
                              "\tPRICE x MULTIPLIER \t-> calculate PRICE for same multiple items; example: '0.5w x 32' returns '0 ref. 2 rec. 2 scr.'");
            break;

        // get price in items 1.66r = 1r 2c
        case [var price, "=", ..var options]:
            Console.WriteLine(GetPriceInItems(
                GetScrapValue(new(price)
            )));
            break;

        // count multiple items
        case [var price, "x", var multiplier, ..var options]:
            var scrap = GetScrapValue(new Price(price));
            Console.WriteLine(GetPriceInItems(
                scrap * uint.Parse(multiplier)
            ));
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

static string GetPriceInItems(double scrapValue)
{
    int roundedValue = (int)Math.Round(scrapValue);

    int r = roundedValue / 9;
    int c = roundedValue % 9 / 3;
    int s = roundedValue % 3;

    return $"{r} ref. {c} rec. {s} scr.";
}

static double GetScrapValue(Price price)
{
    return price.currency switch
    {
        'w' => price.value * 0.5,
        's' => price.value,
        'c' => price.value * 3,
        'r' => price.value * 9,
        _ => throw new ArgumentException($"invalid currency '{price.currency}'")
    };
}

readonly struct Price(string price)
{
    public readonly char currency = price.Last();
    public readonly double value = double.Parse(price[..^1]);
}
