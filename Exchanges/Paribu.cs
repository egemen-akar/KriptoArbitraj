using System;
using System.Collections.Generic;
using System.Globalization;

namespace KriptoArbitraj
{
    static class Paribu
    {
        private static readonly string apiEndpoint = @"https://v3.paribu.com/app/markets/";
        private static Dictionary<CurrencyPair, string> pairSymbols = new()
        {
            { new() { Primary = CurrencySymbol.BTC, Secondary = CurrencySymbol.TRY }, "btc-tl" },
            { new() { Primary = CurrencySymbol.ETH, Secondary = CurrencySymbol.TRY }, "eth-tl" },
            { new() { Primary = CurrencySymbol.USDT, Secondary = CurrencySymbol.TRY }, "usdt-tl" }
        };
        private static List<Order> Unpack(DateTime time, CurrencyPair pair, string apiResponse)
        {
            List<Order> orderBook = new();
            var words = apiResponse.Split('{', '}');
            int index = 0;
            while ((words[index].Contains("buy") || words[index].Contains("sell")) == false) { index++; }
            string[] buywords = null;
            string[] sellwords = null;
            if (words[index].Contains("buy") && words[index + 2].Contains("sell"))
            {
                buywords = words[index + 1].Split(',');
                sellwords = words[index + 3].Split(',');
            }
            else if(words[index].Contains("sell") && words[index + 2].Contains("buy"))
            {
                buywords = words[index + 3].Split(',');
                sellwords = words[index + 1].Split(',');
            }
            foreach (var buyword in buywords)
            {
                var pairWords = buyword.Split(':');
                var rate = decimal.Parse(pairWords[0].Trim('\"'), NumberStyles.Float);
                var volume = decimal.Parse(pairWords[1], NumberStyles.Float);
                var order = new Order
                {
                    Exchange = ExchangeName.Paribu,
                    Time = time,
                    Pair = pair,
                    Type = OrderType.Ask,
                    Rate = rate,
                    Volume = volume
                };
                orderBook.Add(order);
            }
            foreach (var sellword in sellwords)
            {
                var pairWords = sellword.Split(':');
                var rate = decimal.Parse(pairWords[0].Trim('\"'), NumberStyles.Float);
                var volume = decimal.Parse(pairWords[1], NumberStyles.Float);
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
            Console.WriteLine("Done");
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