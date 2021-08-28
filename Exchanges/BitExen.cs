// using System;
// using System.Collections.Generic;
// using System.Text.Json;

// namespace KriptoArbitraj
// {
//     public static class BitExen
//     {
//         private static string apiEndpoint = @"https://www.bitexen.com/api/v1/order_book/";
//         private static Dictionary<CurrencyPair, string> pairSymbols = new()
//         {
//             { new() { Primary = CurrencySymbol.BTC, Secondary = CurrencySymbol.TRY }, "BTCTRY/" },
//             { new() { Primary = CurrencySymbol.ETH, Secondary = CurrencySymbol.TRY }, "ETHTRY/" },
//             { new() { Primary = CurrencySymbol.USDT, Secondary = CurrencySymbol.TRY }, "USDTTRY/" }
//         };
//         public class Root
//         {
//             public string status { get; set; }
//             public Data data { get; set; }
//         }
//         public class Data
//         {
//             public string market_code { get; set; }
//             public Ticker ticker { get; set; }
//             public List<Buyer> buyers { get; set; }
//             public List<Seller> sellers { get; set; }
//             public List<LastTransaction> last_transactions { get; set; }
//             public string timestamp { get; set; }
//         }
//         public class LastTransaction
//         {
//             public string amount { get; set; }
//             public string price { get; set; }
//             public string time { get; set; }
//             public string type { get; set; }
//         }
//         public class Seller
//         {
//             public string orders_total_amount { get; set; }
//             public string orders_price { get; set; }
//         }
//         public class Buyer
//         {
//             public string orders_total_amount { get; set; }
//             public string orders_price { get; set; }
//         }
//         public class Ticker
//         {
//             public Market market { get; set; }
//             public string bid { get; set; }
//             public string ask { get; set; }
//             public string last_price { get; set; }
//             public string last_size { get; set; }
//             public string volume_24h { get; set; }
//             public string change_24h { get; set; }
//             public string low_24h { get; set; }
//             public string high_24h { get; set; }
//             public string avg_24h { get; set; }
//             public string timestamp { get; set; }
//         }
//         public class Market
//         {
//             public string market_code { get; set; }
//             public string base_currency_code { get; set; }
//             public string counter_currency_code { get; set; }
//         }
//     }
// }