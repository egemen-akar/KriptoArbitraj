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

        public static void FindArbitrage()
        {
            //reset list and indexes
            State.Opportunuties.Clear();
            var askRanking = (from orders in State.OrdersPile
                              where orders.Type == OrderType.Ask
                              orderby orders.Rate ascending
                              select orders)
                .Take(Configuration.Depth)
                .ToList();
            var bidRanking = (from orders in State.OrdersPile
                              where orders.Type == OrderType.Bid
                              orderby orders.Rate descending
                              select orders)
                .Take(Configuration.Depth)
                .ToList();
            if(Configuration.DiagMode == true)
            {
                Diagnostics.Sort();
            }
            int askindex = 0;
            int bidindex = 0;
            while (askindex < Configuration.Depth)
            {
                while (bidindex < Configuration.Depth)
                {
                    //shorthand variables
                    var currentAsk = askRanking[askindex];
                    var currentBid = bidRanking[bidindex];

                    //this if-else statement determines whether there is an opportunity
                    if (currentAsk.Rate < currentBid.Rate)
                    {
                        var currentDelta = currentBid.Rate - currentAsk.Rate;
                        var currentEpsilon = currentDelta / currentAsk.Rate;
                        var epsilonCondition = currentEpsilon > Configuration.MinEpsilon;
                        var deltaCondition = currentDelta > Configuration.MinDelta;
                        var profitableDelta = currentDelta - Configuration.MinDelta;
                        var profitableEpsilon = currentEpsilon - Configuration.MinEpsilon;

                        //this if-else statement determines the limiting side
                        if (currentAsk.Volume <= currentBid.Volume)
                        {
                            var volumeCondition = currentAsk.Volume > Configuration.MinVolume;
                            var profitableVol = currentAsk.Volume - Configuration.MinVolume;
                            var currentProfit_Delta = profitableDelta * profitableVol;
                            var currentProfit_Epsilon = profitableEpsilon * profitableVol * currentAsk.Rate;
                            Decimal currentProfit;
                            if (currentProfit_Epsilon < currentProfit_Delta) { currentProfit = currentProfit_Epsilon; }
                            else { currentProfit = currentProfit_Delta; }
                            var profitCondition = currentProfit_Delta > Configuration.MinProfit && currentProfit_Epsilon > Configuration.MinProfit;
                            if ((deltaCondition && epsilonCondition && volumeCondition && profitCondition) == true)
                            {
                                var firstAction = new Action()
                                {
                                    Type = ActionType.Buy,
                                    Exchange = currentAsk.Exchange,
                                    Volume = profitableVol
                                };
                                var secondAction = new Action()
                                {
                                    Type = ActionType.Sell,
                                    Exchange = currentBid.Exchange,
                                    Volume = profitableVol
                                };
                                var opportunity = new Arbitrage()
                                {
                                    Delta = profitableDelta,
                                    Epsilon = profitableEpsilon,
                                    Profit = currentProfit,
                                    Actions = new() { firstAction, secondAction }
                                };
                                State.Opportunuties.Add(opportunity);
                            }
                        }
                        else
                        {
                            var volumeCondition = currentBid.Volume > Configuration.MinVolume;
                            var profitableVol = currentBid.Volume - Configuration.MinVolume;
                            var currentProfit_Delta = profitableDelta * profitableVol;
                            var currentProfit_Epsilon = profitableEpsilon * profitableVol * currentAsk.Rate;
                            Decimal currentProfit;
                            if (currentProfit_Epsilon < currentProfit_Delta) { currentProfit = currentProfit_Epsilon; }
                            else { currentProfit = currentProfit_Delta; }
                            var profitCondition = currentProfit_Delta > Configuration.MinProfit && currentProfit_Epsilon > Configuration.MinProfit;
                            if ((deltaCondition && epsilonCondition && volumeCondition && profitCondition) == true)
                            {
                                var firstAction = new Action()
                                {
                                    Type = ActionType.Sell,
                                    Exchange = currentBid.Exchange,
                                    Volume = profitableVol
                                };
                                var secondAction = new Action()
                                {
                                    Type = ActionType.Buy,
                                    Exchange = currentAsk.Exchange,
                                    Volume = profitableVol
                                };
                                var opportunity = new Arbitrage()
                                {
                                    Delta = profitableDelta,
                                    Epsilon = profitableEpsilon,
                                    Profit = currentProfit,
                                    Actions = new() { firstAction, secondAction }
                                };
                                State.Opportunuties.Add(opportunity);
                            }
                        }
                    }
                    bidindex++;
                }
                askindex++;
            }
            State.Opportunuties = (from opportunity in State.Opportunuties
                orderby opportunity.Profit descending, opportunity.Actions[0].Volume ascending
                select opportunity)
                .ToList();
        }
    }
}