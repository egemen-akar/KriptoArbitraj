using System;
using System.Collections.Generic;
using System.Text.Json;

namespace KriptoArbitraj
{
    public static class Bitci
    {
        private static readonly string apiEndpoint = @"https://api.bitci.com/api/orderbook/";
        private static readonly Dictionary<CurrencyPair, string> pairSymbols = new()
        {
            { new() { Primary = CurrencySymbol.BTC, Secondary = CurrencySymbol.TRY }, "BTC_TRY" },
            { new() { Primary = CurrencySymbol.ETH, Secondary = CurrencySymbol.TRY }, "ETH_TRY" },
            //USDT not available
            { new() { Primary = CurrencySymbol.HOT, Secondary = CurrencySymbol.TRY }, "HOT_TRY" },
            { new() { Primary = CurrencySymbol.LINK, Secondary = CurrencySymbol.TRY }, "LINKTRY" },
            { new() { Primary = CurrencySymbol.XRP, Secondary = CurrencySymbol.TRY }, "XRPTRY" },
            { new() { Primary = CurrencySymbol.ADA, Secondary = CurrencySymbol.TRY }, "ADATRY" }
        };
        private class Dto
        {
            public long lastUpdatedTimestamp { get; set; }
            public Ask[] asks { get; set; }
            public Bid[] bids { get; set; }
        }
        private class Ask
        {
            public Decimal quantity { get; set; }
            public Decimal price { get; set; }
        }
        private class Bid
        {
            public Decimal quantity { get; set; }
            public Decimal price { get; set; }
        }
        private static List<Order> Unpack(DateTime time, CurrencyPair pair, string apiResponse)
        {
            List<Order> orderBook = new();
            var dto = JsonSerializer.Deserialize<Dto>(apiResponse);
            foreach (var ask in dto.asks)
            {
                var order = new Order()
                {
                    Exchange = ExchangeName.Bitci,
                    Time = time,
                    Pair = pair,
                    Type = OrderType.Ask,
                    Rate = ask.price,
                    Volume = ask.quantity
                };
                orderBook.Add(order);
            }
            foreach (var bid in dto.bids)
            {
                var order = new Order()
                {
                    Exchange = ExchangeName.Bitci,
                    Time = time,
                    Pair = pair,
                    Type = OrderType.Bid,
                    Rate = bid.price,
                    Volume = bid.quantity
                };
                orderBook.Add(order);
            }
            Console.WriteLine("Done");
            return orderBook;
        }
        public static void RunGetTask()
        {
            if (pairSymbols.ContainsKey(Configuration.Pair))
            {
                Utilities.RunApiGetTask(apiEndpoint, pairSymbols[Configuration.Pair], Unpack);
            }
        }
    }
}