# ConsoleCoin

A simple Console App in .NET 5 with makes uses of the awesome library [Spectre.Console](https://github.com/spectresystems/spectre.console), to display a simple table with cryptocurrencies and its prices.
The table is refreshed every minute and data is obtained via [CoinGecko](https://www.coingecko.com/) using the library [CoinGeckoAsyncApi](https://github.com/tosunthex/CoinGecko).

## Build

Simply run in the terminal (root directory of the project):

```
dotnet build
```

## Running

Run (in the directory containing the `csproj` file):

```
dotnet run
```

By default the app will show eight relevant cryptocurrencies and its prices in USD.
This behaviour can be changed, as the app accepts two additional optional arguments (in order):
- Currency to show the prices in (for each coin). Currencies supported as listed in the [CoinGecko endpoint for currencies](https://api.coingecko.com/api/v3/simple/supported_vs_currencies).
- A list of coin identifiers to show in the table (separated by comma). Coin identifiers as listed in the [CoinGecko endpoint for coins](https://api.coingecko.com/api/v3/coins/list).


## License

This project is licensed under the MIT License. Please see the LICENSE document in the root of this repository.