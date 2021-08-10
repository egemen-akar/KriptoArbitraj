using System;

namespace KriptoArbitraj
{
    static class Configuration
    {
        //search
        public static string Pair = "All";
        public static int Depth = 5;
        public static Decimal MinDelta = 0;
        public static Decimal MinEpsilon = 0;
        public static Decimal MinResult = 0;

        //refresh
        public static bool AutoRefresh = false;
        public static int RefreshInterval = 7500;

        //number format
        public static int SignificantDigits = 6;
        public static string RealFormat = "G" + SignificantDigits.ToString();
        public static string ToCompactString(this Decimal num)
        {
            return num.ToString(RealFormat);
        }
        
        //logging
         public static bool DiagMode = true;
    }
}
