using System;
using System.Text;

namespace KriptoArbitraj
{
    public class Order
    {
        public ExchangeName Exchange;
        public DateTime Time;
        public CurrencyPair Pair;
        public OrderType Type;
        public Decimal Rate;
        public Decimal Volume;
        public override String ToString()
        {
            StringBuilder sb = new();
            var volStr = Volume.ToCompactString();
            var rateStr = Rate.ToCompactString();
            sb.AppendFormat($"Pair: {Pair}, Exchange: {Exchange}, Time: {Time} \n");
            sb.AppendFormat($"Type: {Type}, Rate: {rateStr}, Volume: {volStr}");
            return sb.ToString();
        }
    }
    public struct CurrencyPair
    {
        public CurrencySymbol Primary;
        public CurrencySymbol Secondary;
        public override String ToString()
        {
            return $"{Primary}-{Secondary}";
        }
    }
    public enum CurrencySymbol { TRY, USD, BTC, ETH, USDT, HOT }
    public enum ExchangeName { Binance, Bitci, BtcTurk, Paribu }
    public enum OrderType { Ask, Bid }
}