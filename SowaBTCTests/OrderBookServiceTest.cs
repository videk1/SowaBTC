using Microsoft.VisualStudio.TestTools.UnitTesting;
using SowaBTC;
using SowaBTC.Services.OrderBookService;
using System.Collections.Generic;
using Xunit;

namespace SowaBTCTests
{
    [TestClass]
    public class OrderBookServiceTest
    {
        private readonly IOrderBookService _orderBookService = new OrderBookService();

        [TestMethod]
        public void ShouldReturnEmptySets()
        {
            var testData = new BitstampOrderBook()
            {
                data = new BitstampOrderBookData()
                {
                    asks = new List<string[]>(),
                    bids = new List<string[]>()
                }
            };
            var response = _orderBookService.CalculateAndReturnOrderBookData(testData);
            Xunit.Assert.Equal(testData.data.bids.Count, response.bids.Count);
            Xunit.Assert.Equal(testData.data.asks.Count, response.asks.Count);
        }

        [TestMethod]
        public void ShouldCalculateTheRightMaxToBuyValue()
        {
            var asks = new List<string[]>();
            var bids = new List<string[]>();

            decimal totalValue = 0;

            for(decimal i = 1; i < 11; i++)
            {
                totalValue += i+1;
                asks.Add(new string[] { i.ToString(), (i + 1).ToString() });
                bids.Add(new string[] { i.ToString(), (i + 2).ToString() });
            }
            
            var testData = new BitstampOrderBook()
            {
                data = new BitstampOrderBookData()
                {
                    asks = asks,
                    bids = bids
                }
            };

            var response = _orderBookService.CalculateAndReturnOrderBookData(testData);
            Xunit.Assert.Equal(totalValue, response.maxBtcToBuy);
        }

        [TestMethod]
        public void ShouldGroupDataWithSamePrice()
        {
            var asks = new List<string[]>();
            var bids = new List<string[]>();

            decimal askPrice = 10;
            decimal askAmount1 = 5;

            decimal askAmount2 = 15;

            asks.Add(new string[] { askPrice.ToString(), askAmount1.ToString() });
            asks.Add(new string[] { askPrice.ToString(), askAmount2.ToString() });

            var testData = new BitstampOrderBook()
            {
                data = new BitstampOrderBookData()
                {
                    asks = asks,
                    bids = bids
                }
            };

            var response = _orderBookService.CalculateAndReturnOrderBookData(testData);
            Xunit.Assert.Equal(1, response.asks.Count);
            Xunit.Assert.Equal(askAmount1 + askAmount2, response.asks[0][1]);
        }
    }
}
