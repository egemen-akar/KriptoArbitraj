using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace KriptoArbitraj
{
    static class Utilities
    {
        public static Stopwatch Chronometer = new();
        public static Timer Timer = new();
        public static HttpClient Client = new();
        public static async Task GetOrdersFromApiAsync(string apiEndpoint,
            Dictionary<CurrencySymbol[], string> pairs,
            Func<DateTime, CurrencySymbol[], string, List<Order>> unpacker)
        {
            List<Task> tasks = new();
            foreach (var pair in pairs)
            {
                var time = DateTime.Now;
                var currencies = pair.Key;
                var url = apiEndpoint + pair.Value;
                async Task Action()
                {
                    var response = await Utilities.Client.GetAsync(url);
                    var content = await response.Content.ReadAsStringAsync();
                    var data = unpacker(time, currencies, content);
                    State.orders.AddRange(data);
                }
                var task = Task.Run(Action);
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
            return;
        }
        // }
        // public static void ArbitrageSearch(CurrencySymbol[] currencies)
        // {
        //     //reset list and indexes
        //     Arbitrages.Clear();
        //     int askindex = 0;
        //     int bidindex = 0;

        //     while (askindex < Configuration.SearchDepth && bidindex < Configuration.SearchDepth)
        //     {
        //         //shorthand variables
        //         var currentAsk = askRanking[askindex];
        //         var currentBid = bidRanking[bidindex];

        //         //this if-else statement determines whether there is an opportunity
        //         if (currentAsk.Rate < currentBid.Rate)
        //         {
        //             double currentDelta = currentBid.Rate - currentAsk.Rate;
        //             double currentEpsilon = currentDelta / currentAsk.Rate;

        //             //this if-else statement determines the limiting side
        //             if (currentAsk.Volume < currentBid.Volume)
        //             {
        //                 var opportunity = new Arbitrage(firstCurr, secondCurr, currentAsk.Exchange, currentBid.Exchange,
        //                     currentAsk.Rate, currentBid.Rate, "ask", currentAsk.Volume);

        //                 //determine if the opportunity is good enough
        //                 double currentResult = currentDelta * currentAsk.Volume;
                        
        //                 if(Configuration.MinDelta < currentDelta && Configuration.MinEpsilon < currentEpsilon && Configuration.MinResult < currentResult)
        //                 {
        //                     Arbitrages.Add(opportunity);
        //                     currentBid.Volume -= currentAsk.Volume;
        //                     askindex++;
        //                 }
                        
        //                 else { break; }
        //             }

        //             else if (currentBid.Volume < currentAsk.Volume)
        //             {
        //                 var opportunity = new Arbitrage(firstCurr, secondCurr, currentAsk.Exchange,
        //                     currentBid.Exchange,
        //                     currentAsk.Rate, currentBid.Rate, "bid", currentBid.Volume);

        //                 double currentResult = currentDelta * currentBid.Volume;

        //                 if (Configuration.MinDelta < currentDelta && Configuration.MinEpsilon < currentEpsilon && Configuration.MinResult < currentResult)
        //                 {
        //                     Arbitrages.Add(opportunity);
        //                     currentAsk.Volume -= currentBid.Volume;
        //                     bidindex++;
        //                 }

        //                 else { break; }
        //             }
        //         }

        //         else { break; }
        //     }
        // }
    }
}