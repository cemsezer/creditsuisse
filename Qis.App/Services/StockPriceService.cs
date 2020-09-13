using Newtonsoft.Json;
using Qis.App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Qis.App.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly HttpClient _client;
        private readonly string Uri = "https://credit-suisse-qis.firebaseio.com/prices.json";

        public StockPriceService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Stock>> GetStockPricessByDateAsync(DateTime date)
        {
            var response = await _client.GetAsync(Uri);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var allStockPrices = JsonConvert.DeserializeObject<List<Stock>>(jsonResponse);
            return allStockPrices.Where(s => s.Date == date);
        }
    }
}
