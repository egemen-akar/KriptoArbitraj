using System;
using static System.Console;

namespace KriptoArbitraj
{
    static class Tests
    {
        public static void PrintOrderBooks()
        {
            var time = Utilities.Chronometer.ElapsedMilliseconds;
            WriteLine($"there are {State.orders.Count} orders");
            WriteLine($"took {time} miliseconds up to this point");
        }
    }
}