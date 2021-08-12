using System;
using static System.Console;

namespace KriptoArbitraj
{
    static class Configuration
    {
        public static CurrencyPair Pair;
        //search
        public static int Depth = 5;
        public static Decimal MinEpsilon = 0;
        public static Decimal MinVolume = 0;
        public static Decimal MinProfit = 0;
        //refresh
        public static bool AutoRefresh = false;
        public static int RefreshInterval = 5000;
        //number format
        public static int SignificantDigits = 6;
        public static string RealFormat = "G" + SignificantDigits.ToString();
        public static string ToCompactString(this Decimal num)
        {
            return num.ToString(RealFormat);
        }
        //logging
        public static bool DiagMode = true;
        public static void Configure()
        {
            SetPair();
            SetSearchParams();
            
        }
        static void SetPair()
        {
            while (true)
            {
                WriteLine("Select primary");
                int primaryIndex = SelectCurrency();
                if (primaryIndex == -1)
                {
                    WriteLine("Invalid selection");
                    continue;
                }
                WriteLine("Select secondary");
                int secondaryIndex = SelectCurrency();
                if (secondaryIndex == -1)
                {
                    WriteLine("Invalid selection");
                    continue;
                }
                CurrencySymbol primary = (CurrencySymbol)primaryIndex;
                CurrencySymbol secondary = (CurrencySymbol)secondaryIndex;
                Pair = new CurrencyPair() { Primary = primary, Secondary = secondary };
                break;
            }
        }
        static int SelectCurrency()
        {
            int index = 1;
            foreach (var currency in Enum.GetNames(typeof(CurrencySymbol)))
            {
                WriteLine($"{index} : {currency}");
                index++;
            }
            int selected = int.Parse(ReadLine());
            if (Enum.IsDefined(typeof(CurrencySymbol), selected))
            {
                return selected;
            }
            else
            {
                return -1;
            }
        }
        static void SetSearchParams()
        {
            while(true)
            {
                WriteLine("Enter depth, min epsilon, min volume, min profit seperated with one space");
            }
        }
    }
}
