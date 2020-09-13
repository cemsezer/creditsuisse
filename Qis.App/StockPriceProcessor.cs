using Qis.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qis.App
{
    public class StockPriceProcessor
    {
        private readonly IStockPriceService _stockPriceService;

        public StockPriceProcessor(IStockPriceService stockPriceService)
        {
            _stockPriceService = stockPriceService;
        }

        public async Task<Dictionary<string, List<string>>> ProcessAsync(DateTime date)
        {
            var stockPrices = await _stockPriceService.GetStockPricessByDateAsync(date);

            return stockPrices
                .GroupBy(s => s.Exchange)
                .Select(g => new
                {
                    Exchange = g.Key,
                    Symbols = g.Where(x => x.DailyGain == g.OrderByDescending(y => y.DailyGain).First().DailyGain).Select(z => z.Symbol).ToList()
                })
                .ToDictionary(g => g.Exchange, g => g.Symbols);
        }
    }
}