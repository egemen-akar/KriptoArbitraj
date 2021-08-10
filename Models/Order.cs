using System;
using System.Text;

namespace KriptoArbitraj
{
    public class Order
    {
        public ExchangeName Exchange { get; init; }
        public DateTime Time { get; init; }
        public CurrencySymbol[] Pair { get; init; }
        public OrderType Type { get; init; }
        public Decimal Rate { get; init; }
        public Decimal Volume { get; init; }
        public override String ToString()
        {
            StringBuilder sb = new();
            var volStr = Volume.ToCompactString();
            var rateStr = Rate.ToCompactString();
            sb.AppendFormat($"Pair: {Pair[0]}-{Pair[1]}, Exchange: {Exchange}, Time: {Time} \n");
            sb.AppendFormat($"Type: {Type}, Rate: {rateStr}, Volume: {volStr}");
            return sb.ToString();
        }
    }
    public enum CurrencySymbol { TRY, USD, BTC, ETH, USDT }
    public enum ExchangeName { Binance, Bitci, BtcTurk, Paribu }
    public enum OrderType { Ask, Bid }
}