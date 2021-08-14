using System;
using System.Globalization;
using static System.Console;

namespace KriptoArbitraj
{
    static class Configuration
    {
        public static CurrencyPair Pair;
        //search
        public static int Depth = 5;
        public static Decimal MinDelta = 0;
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
            SetDiagMode();
            if (Configuration.DiagMode == false)
            {
                SetSearchParams();
                SetRefresh();
            }
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
            int selected = int.Parse(ReadLine()) - 1;
            if (Enum.IsDefined(typeof(CurrencySymbol), selected))
            {
                return selected;
            }
            else
            {
                return -1;
            }
        }
        static void SetDiagMode()
        {
            while (true)
            {
                WriteLine("Diag mode? (y/n)");
                var option = ReadLine();
                if (option == "n")
                {
                    Configuration.DiagMode = false;
                    break;
                }
                else if (option == "y")
                {
                    Configuration.DiagMode = true;
                    break;
                }
                else
                {
                    WriteLine("Invalid");
                    continue;
                }
            }
        }
        static void SetSearchParams()
        {
            while (true)
            {
                int depth;
                decimal minep;
                decimal mindel;
                decimal minvol;
                decimal minpr;
                WriteLine("Enter depth");
                while (true)
                {
                    depth = int.Parse(ReadLine());
                    if (depth < 1)
                    {
                        WriteLine("Depth must be equal to or greater than 1");
                        continue;
                    }
                    break;
                }
                WriteLine("Enter minimum epsilon");
                while (true)
                {
                    minep = Decimal.Parse(ReadLine(), NumberStyles.Float);
                    if (minep < 0)
                    {
                        WriteLine("Minimum epsilon must be non-negative");
                        continue;
                    }
                    break;
                }
                WriteLine("Enter minimum delta");
                while (true)
                {
                    mindel = Decimal.Parse(ReadLine(), NumberStyles.Float);
                    if (mindel < 0)
                    {
                        WriteLine("Minimum delta must be non-negative");
                        continue;
                    }
                    break;
                }
                WriteLine("Enter minimum volume");
                while (true)
                {
                    minvol = Decimal.Parse(ReadLine(), NumberStyles.Float);
                    if (minvol < 0)
                    {
                        WriteLine("Minimum volume must be non-negative");
                        continue;
                    }
                    break;
                }
                WriteLine("Enter minimum profit");
                while (true)
                {
                    minpr = Decimal.Parse(ReadLine(), NumberStyles.Float);
                    if (minpr < 0)
                    {
                        WriteLine("Minimum profit must be non-negative");
                        continue;
                    }
                    break;
                }
                Depth = depth;
                MinEpsilon = minep;
                MinVolume = minvol;
                MinProfit = minpr;
                break;
            }
        }
        static void SetRefresh()
        {
            while (true)
            {
                WriteLine("Auto refresh? (y/n)");
                var option = ReadLine();
                if (option == "n")
                {
                    Configuration.AutoRefresh = false;
                    break;
                }
                else if (option == "y")
                {
                    WriteLine("Interval? (in miliseconds, default = 5000)");
                    var interval = int.Parse(ReadLine());
                    if (interval < 3000)
                    {
                        WriteLine("Auto refresh interval must be at least 3000");
                        continue;
                    }
                    Configuration.AutoRefresh = true;
                    Configuration.RefreshInterval = interval;
                    break;
                }
                else
                {
                    WriteLine("Invalid");
                    continue;
                }
            }
        }
    }
}
