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
    public static class Bitci
    {
        private static string apiEndpoint = @"https://api.bitci.com/api/orderbook/";
        private static Dictionary<CurrencySymbol[], string> pairs = new()
        {
            { new[] { CurrencySymbol.BTC, CurrencySymbol.TRY }, "BTC_TRY" },
            { new[] { CurrencySymbol.ETH, CurrencySymbol.TRY }, "ETH_TRY" },
        };
        private class Dto
        {
            public long lastUpdatedTimestamp { get; set; }
            public Ask[] asks { get; set; }
            public Bid[] bids { get; set; }
        }
        private class Ask
        {
            public Decimal qty { get; set; }
            public Decimal price { get; set; }
        }
        private class Bid
        {
            public Decimal qty { get; set; }
            public Decimal price { get; set; }
        }
        private static List<Order> Unpack(DateTime time, CurrencySymbol[] currencies, string apiResponse)
        {
            List<Order> orderBook = new();
            var dto = JsonSerializer.Deserialize<Dto>(apiResponse);
            foreach (var ask in dto.asks)
            {
                var order = new Order()
                {
                    Exchange = ExchangeName.Bitci,
                    Time = time,
                    Pair = currencies,
                    Type = OrderType.Ask,
                    Rate = ask.price,
                    Volume = ask.qty
                };
                orderBook.Add(order);
            }
            foreach (var bid in dto.bids)
            {
                var order = new Order()
                {
                    Exchange = ExchangeName.Bitci,
                    Time = time,
                    Pair = currencies,
                    Type = OrderType.Bid,
                    Rate = bid.price,
                    Volume = bid.qty
                };
                orderBook.Add(order);
            }
            Console.WriteLine("Done");
            return orderBook;
        }
        public static async Task GetOrdersAsync()
        {
            await Utilities.GetOrdersFromApiAsync(apiEndpoint, pairs, Unpack);
        }
    }
}