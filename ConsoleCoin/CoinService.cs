using CoinGecko.Clients;
using CoinGecko.Entities.Response.Coins;
using CoinGecko.Entities.Response.Simple;
using CoinGecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleCoin
{
    public class CoinService
    {
        private IReadOnlyList<CoinList> _allCoinsList;
        private readonly ICoinGeckoClient _client;

        private readonly string[] _coinList;
        private readonly string _vsCurrency;

        public DateTime LastUpdate { get; set; }

        public CoinService(string[] coinList, string vsCurrency)
        {
            _client = CoinGeckoClient.Instance;
            _coinList = coinList;
            _vsCurrency = vsCurrency;
            LastUpdate = DateTime.Now;
        }

        public async Task<IReadOnlyList<CoinList>> GetCoins()
        {
            if(_allCoinsList == null || _allCoinsList.Count == 0)
            {
                _allCoinsList = await _client.CoinsClient.GetCoinList();
            }
            return _allCoinsList;
        }

        public async Task<Dictionary<string, double>> GetCoinPrices()
        {
            var result = await _client.SimpleClient.GetSimplePrice(_coinList, new string[] { _vsCurrency });
            LastUpdate = DateTime.Now;
            return PriceToDictionary(result);
        }

        private Dictionary<string, double> PriceToDictionary(Price prices)
        {
            var result = new Dictionary<string, double>();
            foreach (var coin in prices)
            {
                result.Add(coin.Key, coin.Value[_vsCurrency].Value);
            }
            return result.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
