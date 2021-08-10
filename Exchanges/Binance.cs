using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;
using System.Globalization;
using static System.Console;
using static System.Timers.Timer;

namespace KriptoArbitraj
{
    public static class Binance
    {
        private static string apiEndpoint = @"https://api.binance.com/api/v3/depth?symbol=";
        private static Dictionary<CurrencySymbol[], string> pairs = new()
        {
            { new[] { CurrencySymbol.BTC, CurrencySymbol.TRY }, "BTCTRY" },
            { new[] { CurrencySymbol.ETH, CurrencySymbol.TRY }, "ETHTRY" },
            { new[] { CurrencySymbol.USDT, CurrencySymbol.TRY }, "USDTTRY" }

        };
        private class Dto
        {
            public int lastUpdateId { get; set; }
            public string[][] bids { get; set; }
            public string[][] asks { get; set; }
        }
        private static List<Order> Unpack(DateTime time, CurrencySymbol[] currencies, string apiResponse)
        {
            List<Order> orderBook = new();
            var dto = JsonSerializer.Deserialize<Dto>(apiResponse);
            var askCount = dto.asks.GetLength(0);
            for (int i = 0; i < askCount; i++)
            {
                var rate = Decimal.Parse(dto.asks[i][0], NumberStyles.Float);
                var volume = Decimal.Parse(dto.asks[i][1], NumberStyles.Float);
                var order = new Order
                {
                    Exchange = ExchangeName.Binance,
                    Time = time,
                    Pair = currencies,
                    Type = OrderType.Ask,
                    Rate = rate,
                    Volume = volume
                };
                orderBook.Add(order);
            }
            var bidCount = dto.bids.GetLength(0);
            for (int i = 0; i < bidCount; i++)
            {
                var rate = Decimal.Parse(dto.bids[i][0], NumberStyles.Float);
                var volume = Decimal.Parse(dto.bids[i][1], NumberStyles.Float);
                var order = new Order
                {
                    Exchange = ExchangeName.Binance,
                    Time = time,
                    Pair = currencies,
                    Type = OrderType.Bid,
                    Rate = rate,
                    Volume = volume
                };
                orderBook.Add(order);
            }
            return orderBook;
        }
        public static async Task GetOrdersAsync()
        {
            await Utilities.GetOrdersFromApiAsync(apiEndpoint, pairs, Unpack);
        }
    }
}