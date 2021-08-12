using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

namespace KriptoArbitraj
{
    public static class BtcTurk
    {
        private static string apiEndpoint = @"https://api.btcturk.com/api/v2/orderbook?pairSymbol=";
        private static Dictionary<CurrencyPair, string> pairSymbols = new()
        {
            { new() { Primary = CurrencySymbol.BTC,  Secondary = CurrencySymbol.TRY }, "BTC_TRY" },
            { new() { Primary = CurrencySymbol.ETH,  Secondary = CurrencySymbol.TRY }, "ETH_TRY" },
            { new() { Primary = CurrencySymbol.USDT,  Secondary = CurrencySymbol.TRY }, "USDT_TRY" }
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
        private static List<Order> Unpack(DateTime time, CurrencyPair pair, string apiResponse)
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
                    Pair = pair,
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
                    Pair = pair,
                    Type = OrderType.Bid,
                    Rate = rate,
                    Volume = volume
                };
                orderBook.Add(order);
            }
            return orderBook;
        }
        public static void RunGetTask()
        {
            Utilities.RunApiGetTasks(apiEndpoint, pairSymbols, Unpack);
        }
    }
}