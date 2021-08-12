using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

namespace KriptoArbitraj
{
    public static class Binance
    {
        private static string apiEndpoint = @"https://api.binance.com/api/v3/depth?symbol=";
        private static Dictionary<CurrencyPair, string> pairSymbols = new()
        {
            { new() { Primary = CurrencySymbol.BTC,  Secondary = CurrencySymbol.TRY }, "BTCTRY" },
            { new() { Primary = CurrencySymbol.ETH,  Secondary = CurrencySymbol.TRY }, "ETHTRY" },
            { new() { Primary = CurrencySymbol.USDT,  Secondary = CurrencySymbol.TRY }, "USDTTRY" }
        };
        private class Dto
        {
            public int lastUpdateId { get; set; }
            public string[][] bids { get; set; }
            public string[][] asks { get; set; }
        }
        private static List<Order> Unpack(DateTime time, CurrencyPair pair, string apiResponse)
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
                    Pair = pair,
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
            if(pairSymbols.ContainsKey(Configuration.Pair))
            {
                Utilities.RunApiGetTask(apiEndpoint, pairSymbols[Configuration.Pair], Unpack);
            }
        }
    }
}