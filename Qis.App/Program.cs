using Newtonsoft.Json;
using Qis.App.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Qis.App
{
    class Program
    {
        static private StockPriceProcessor _processor;

        static Program()
        {
            var client = new HttpClient();
            _processor = new StockPriceProcessor(new StockPriceService(client));
        }

        static async Task Main(string[] args)
        {
            do
            {
                Console.WriteLine("Enter a date: ");

                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    var result = await _processor.ProcessAsync(date);
                    var json = JsonConvert.SerializeObject(result);
                    Console.WriteLine(json);
                }
                else
                {
                    Console.WriteLine("You have entered an incorrect value.");
                }

            } while (true);

        }
    }
}