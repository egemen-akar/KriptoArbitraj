using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace KriptoArbitraj
{
    static class Utilities
    {
        public static Stopwatch Chronometer = new();
        public static HttpClient Client = new();
        public static void RunApiGetTask(string apiEndpoint, string symbol,
            Func<DateTime, CurrencyPair, string, List<Order>> unpacker)
        {
            var timeStamp = DateTime.Now;
            var url = apiEndpoint + symbol;
            var task = Task.Run(Action);
            State.GetTasks.Add(task);
            async Task<List<Order>> Action()
            {
                var response = await Utilities.Client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                var data = unpacker(timeStamp, Configuration.Pair, content);
                return data;
            }
        }

        // public static void FindArbitrage_Old(CurrencySymbol[] currencies)
        // {
        //     //reset list and indexes
        //     State.Opportunuties.Clear();
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