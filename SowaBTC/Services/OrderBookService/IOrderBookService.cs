using SowaBTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SowaBTC.Services.OrderBookService
{
    public interface IOrderBookService
    {
        OrderBookDto CalculateAndReturnOrderBookData(BitstampOrderBook bitstampOrderBook);
    }
}
