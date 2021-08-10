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
    static class Paribu
    {
        private static readonly string apiEndpoint = @"https://v3.paribu.com/app/markets/";
        private static Dictionary<CurrencySymbol[], string> pairs = new()
        {
            { new[] { CurrencySymbol.BTC, CurrencySymbol.TRY }, "btc-tl" },
            { new[] { CurrencySymbol.ETH, CurrencySymbol.TRY }, "eth-tl" },
            { new[] { CurrencySymbol.USDT, CurrencySymbol.TRY }, "usdt-tl" }
        };
        private static List<Order> Unpack(DateTime time, CurrencySymbol[] currencies, string apiResponse)
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
                var pair = buyword.Split(':');
                var rate = decimal.Parse(pair[0].Trim('\"'), NumberStyles.Float);
                var volume = decimal.Parse(pair[1], NumberStyles.Float);
                var order = new Order
                {
                    Exchange = ExchangeName.Paribu,
                    Time = time,
                    Pair = currencies,
                    Type = OrderType.Ask,
                    Rate = rate,
                    Volume = volume
                };
                orderBook.Add(order);
            }
            foreach (var sellword in sellwords)
            {
                var pair = sellword.Split(':');
                var rate = decimal.Parse(pair[0].Trim('\"'), NumberStyles.Float);
                var volume = decimal.Parse(pair[1], NumberStyles.Float);
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
            Console.WriteLine("Done");
            return orderBook;
        }
        public static async Task GetOrdersAsync()
        {
            await Utilities.GetOrdersFromApiAsync(apiEndpoint, pairs, Unpack);
        }
    }
}