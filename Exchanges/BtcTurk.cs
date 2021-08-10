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
    public static class BtcTurk
    {
        private static string apiEndpoint = @"https://api.btcturk.com/api/v2/orderbook?pairSymbol=";
        private static Dictionary<CurrencySymbol[], string> pairs = new()
        {
            { new[] { CurrencySymbol.BTC, CurrencySymbol.TRY }, "BTC_TRY" },
            { new[] { CurrencySymbol.ETH, CurrencySymbol.TRY }, "ETH_TRY" },
            { new[] { CurrencySymbol.USDT, CurrencySymbol.TRY }, "USDT_TRY" }
        };
        private class Dto
        {
            public Data data { get; set; }
            public bool success { get; set; }
            public object message { get; set; }
            public int code { get; set; }
        }
        private class Data
        {
            public float timestamp { get; set; }
            public string[][] bids { get; set; }
            public string[][] asks { get; set; }
        }
        private static List<Order> Unpack(DateTime time, CurrencySymbol[] currencies, string apiResponse)
        {
            List<Order> orderBook = new();
            var data = JsonSerializer.Deserialize<Dto>(apiResponse).data;
            var askCount = data.asks.GetLength(0);
            for (int i = 0; i < askCount; i++)
            {
                var rate = Decimal.Parse(data.asks[i][0], NumberStyles.Float);
                var volume = Decimal.Parse(data.asks[i][1], NumberStyles.Float);
                var order = new Order
                {
                    Exchange = ExchangeName.BtcTurk,
                    Time = time,
                    Pair = currencies,
                    Type = OrderType.Ask,
                    Rate = rate,
                    Volume = volume
                };
                orderBook.Add(order);
            }
            var bidCount = data.bids.GetLength(0);
            for (int i = 0; i < bidCount; i++)
            {
                var rate = Decimal.Parse(data.bids[i][0], NumberStyles.Float);
                var volume = Decimal.Parse(data.bids[i][1], NumberStyles.Float);
                var order = new Order
                {
                    Exchange = ExchangeName.BtcTurk,
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