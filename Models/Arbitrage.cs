using System;
using System.Text;
using System.Collections.Generic;

namespace KriptoArbitraj
{
    public class Arbitrage
    {
        public Decimal Delta { get; init; }
        public Decimal Epsilon { get; init; }
        public Decimal Profit { get; init; }
        public List<Action> Actions { get; init; }
        public override String ToString()
        {
            StringBuilder sb = new();
            var profitStr = Profit.ToCompactString();
            var epsilonStr = Epsilon.ToCompactString();
            var deltaStr = Delta.ToCompactString();
            sb.AppendFormat($"For {profitStr} profit (Epsilon: {epsilonStr}, Delta: {deltaStr})");
            foreach(var action in Actions)
            {
                sb.AppendFormat($"\n{action.ToString()}");
            }
            return sb.ToString();
        }
    }
    public class Action
    {
        public ActionType Type { get; init; }
        public ExchangeName Exchange { get; init; }
        public Decimal Volume { get; init; }
        public override String ToString()
        {
            StringBuilder sb = new();
            var volStr = Volume.ToCompactString();
            sb.AppendFormat($"In {Exchange}: {Type} {volStr} {Configuration.Pair.Primary} in exchange to {Configuration.Pair.Secondary}");
            return sb.ToString(); 
        }
    }
    public enum ActionType { Buy, Sell }
}