using Moq;
using Newtonsoft.Json;
using Qis.App.Domain;
using Qis.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Qis.App.Tests
{
    public class StockPriceServiceTests
    {
        private readonly Mock<FakeHttpMessageHandler> _messageHandler;

        public StockPriceServiceTests()
        {
            _messageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
        }

        [Fact]
        public async Task StockPriceService_Should_Return_Result_For_A_Single_Day()
        {
            // Arrange
            var date = DateTime.Now;

            var stocks = new List<Stock>
            {
                new Stock
                {
                    Date = date,
                    Exchange = "XNYS",
                    Close = 2,
                    Open = 1,
                    Symbol = "EVR"
                },
                new Stock
                {
                    Date = date.AddDays(-1),
                    Exchange = "XNYS",
                    Close = 3,
                    Open = 1,
                    Symbol = "MSFT"
                }
            };

            _messageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(stocks))
            });

            var sut = new StockPriceService(new HttpClient(_messageHandler.Object));

            // Act
            var result = await sut.GetStockPricessByDateAsync(date);

            // Assert
            Assert.Single(result.ToList());
            Assert.Equal("XNYS", result.First().Exchange);
        }
    }
}
