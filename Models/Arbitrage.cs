using System;
using System.Text;
using System.Collections.Generic;

namespace KriptoArbitraj
{
    public class Arbitrage
    {
        public Decimal Delta { get; }
        public Decimal Epsilon { get; }
        public Decimal Profit { get; }
        public List<Action> Actions { get; }
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
        public ActionType Type { get; }
        public ExchangeName Exchange { get; }
        public CurrencySymbol[] Pair { get; }
        public Decimal Volume { get; }
        public override String ToString()
        {
            StringBuilder sb = new();
            var volStr = Volume.ToCompactString();
            sb.AppendFormat($"In {Exchange}: {Type} {volStr} {Pair[0]} in exchange to {Pair[1]}");
            return sb.ToString(); 
        }
    }
    public enum ActionType { Buy, Sell }
}