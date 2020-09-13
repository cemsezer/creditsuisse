using Moq;
using Qis.App.Domain;
using Qis.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Qis.App.Tests
{
    public class StockPriceProcessorTests
    {
        private readonly DateTime _date;
        private readonly Mock<IStockPriceService> _serviceMock;

        public StockPriceProcessorTests()
        {
            _date = DateTime.Now;
            _serviceMock = new Mock<IStockPriceService>();
        }

        [Fact]
        public async Task ProcessAsync_Should_Return_EmptyList_When_There_Is_No_Stock_For_A_Day()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetStockPricessByDateAsync(It.IsAny<DateTime>())).ReturnsAsync(new List<Stock>());
            
            var sut = new StockPriceProcessor(_serviceMock.Object);

            // Act
            var response = await sut.ProcessAsync(_date);

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public async Task ProcessAsync_Should_Return_Symbols_Of_One_Exchange_With_LargestGain()
        {
            // Arrange
            var stocks = new List<Stock>
            {
                new Stock
                {

                    Exchange = "XNYS",
                    Close = 2,
                    Open = 1,
                    Symbol = "EVR"
                },
                new Stock
                {

                    Exchange = "XNYS",
                    Close = 3,
                    Open = 1,
                    Symbol = "MSFT"
                }
            };

            _serviceMock.Setup(s => s.GetStockPricessByDateAsync(It.IsAny<DateTime>())).ReturnsAsync(stocks);

            var sut = new StockPriceProcessor(_serviceMock.Object);

            // Act
            var response = await sut.ProcessAsync(_date);

            // Assert
            Assert.Single(response);
            Assert.Equal("XNYS", response.First().Key);
            Assert.Single(response["XNYS"]);
            Assert.Equal("MSFT", response["XNYS"].First());
        }

        [Fact]
        public async Task ProcessAsync_Should_Return_Symbols_Of_Multiple_Exchanges_With_LargestGain()
        {
            // Arrange
            var stocks = new List<Stock>
            {
                new Stock
                {

                    Exchange = "XNYS",
                    Close = 3,
                    Open = 1,
                    Symbol = "EVR"
                },
                new Stock
                {

                    Exchange = "XNYS",
                    Close = 2,
                    Open = 1,
                    Symbol = "BP"
                },
                new Stock
                {

                    Exchange = "XNAS",
                    Close = 5,
                    Open = 1,
                    Symbol = "MSFT"
                },
                new Stock
                {

                    Exchange = "XNAS",
                    Close = 4,
                    Open = 1,
                    Symbol = "AAPL"
                }
            };

            _serviceMock.Setup(s => s.GetStockPricessByDateAsync(It.IsAny<DateTime>())).ReturnsAsync(stocks);

            var sut = new StockPriceProcessor(_serviceMock.Object);

            // Act
            var response = await sut.ProcessAsync(_date);

            // Assert
            Assert.Equal(2, response.Count);
            Assert.True(response.ContainsKey("XNYS"));
            Assert.Single(response["XNYS"]);
            Assert.Equal("EVR", response["XNYS"].First());
            Assert.True(response.ContainsKey("XNAS"));
            Assert.Single(response["XNAS"]);
            Assert.Equal("MSFT", response["XNAS"].First());
        }

        [Fact]
        public async Task ProcessAsync_Should_Return_Multiple_Symbols_Of_Multiple_Exchanges_With_LargestGain()
        {
            // Arrange
            var stocks = new List<Stock>
            {
                new Stock
                {

                    Exchange = "XNYS",
                    Close = 3,
                    Open = 1,
                    Symbol = "EVR"
                },
                new Stock
                {

                    Exchange = "XNYS",
                    Close = 4,
                    Open = 2,
                    Symbol = "BP"
                },
                new Stock
                {

                    Exchange = "XNAS",
                    Close = 5,
                    Open = 1,
                    Symbol = "MSFT"
                },
                new Stock
                {

                    Exchange = "XNAS",
                    Close = 4,
                    Open = 1,
                    Symbol = "AAPL"
                }
            };

            _serviceMock.Setup(s => s.GetStockPricessByDateAsync(It.IsAny<DateTime>())).ReturnsAsync(stocks);

            var sut = new StockPriceProcessor(_serviceMock.Object);

            // Act
            var response = await sut.ProcessAsync(_date);

            // Assert
            Assert.Equal(2, response.Count);
            Assert.True(response.ContainsKey("XNYS"));
            Assert.Equal(2, response["XNYS"].Count);
            Assert.Contains("EVR", response["XNYS"]);
            Assert.Contains("BP", response["XNYS"]);
            Assert.True(response.ContainsKey("XNAS"));
            Assert.Single(response["XNAS"]);
            Assert.Equal("MSFT", response["XNAS"].First());
        }
    }
}
