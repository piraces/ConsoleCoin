using CoinGecko.Entities.Response.Coins;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleCoin
{
    public class MainTable
    {

        private Table _table;
        private CoinService _coinService;
        private IReadOnlyList<CoinList> _allCoinsList;
        
        private readonly string _vsCurrency;
        private readonly string[] _coinList;

        private readonly Color DEFAULT_BORDER_TABLE_COLOR = new Color(247, 147, 26);

        private const string DEFAULT_VS_CURRENCY = "usd";
        private static readonly string[] DEFAULT_COIN_LIST = { "bitcoin", "ethereum", "ripple", "tether", "litecoin", "bitcoin-cash", "monero", "binancecoin" };

        public MainTable(string vsCurrency = DEFAULT_VS_CURRENCY, string[] coins = null)
        {
            if (coins != null && coins.Length > 0)
            {
                _coinList = coins;
            }
            else
            {
                _coinList = DEFAULT_COIN_LIST;
            }

            _coinService = new CoinService(_coinList, vsCurrency);
            _vsCurrency = vsCurrency;
        }

        public async Task Create()
        {
            _table = new Table();
            if (_allCoinsList == null || _allCoinsList.Count == 0)
            {
                _allCoinsList = await _coinService.GetCoins();
            }

            SetTableStyles();

            var coinPrices = await _coinService.GetCoinPrices();

            _table.Title($"Current Crypto Prices \n-- last update {_coinService.LastUpdate.ToShortDateString()} {_coinService.LastUpdate.ToLongTimeString()} ({Enum.GetName(typeof(DateTimeKind), _coinService.LastUpdate.Kind)}) --")
                .Centered();
            _table.AddColumn("[bold]Currency[/]");
            _table.AddColumn(new TableColumn($"[bold]Current Price (in {_vsCurrency.ToUpperInvariant()})[/]").RightAligned());
            
            _table.Caption = new TableTitle("Press ESC to exit. Data will be refreshed every minute. This data obtained from CoinGecko (responsibility discharged)");

            foreach (var coin in coinPrices)
            {
                var actualCoin = _allCoinsList.FirstOrDefault(x => x.Id == coin.Key);
                _table.AddRow($"{actualCoin.Name} ({actualCoin.Symbol.ToUpperInvariant()})", $"[bold]{coin.Value.ToString("N")} {_vsCurrency.ToUpperInvariant()}[/]");
            }
        }

        public async Task Refresh()
        {
            await Create();
            Console.Clear();
            AnsiConsole.Render(_table);
        }

        public void Render()
        {
            AnsiConsole.Render(_table);
        }

        private void SetTableStyles()
        {
            _table.Expand();
            _table.BorderColor(DEFAULT_BORDER_TABLE_COLOR);
            _table.Border = TableBorder.Rounded;
            _table.Centered();
        }
    }
}
