using System;
using System.Collections.Generic;
using System.Text;

namespace KriptoArbitraj
{
    public class Arbitrage
    {
        public Decimal Delta;
        public Decimal Epsilon;
        public Decimal Profit;
        public List<Action> Actions;
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
        public ActionType Type;
        public ExchangeName Exchange;
        public Decimal Volume;
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