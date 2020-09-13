using Qis.App.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Qis.App
{
    class Program
    {
        static readonly DateTime date = new DateTime(2020, 8, 7);

        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var processor = new StockPriceProcessor(new StockPriceService(client));
            var result = await processor.ProcessAsync(date);
        }
    }
}
