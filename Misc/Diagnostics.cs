using static System.Console;

namespace KriptoArbitraj
{
    static class Diagnostics
    {
        public static void Reset()
        {
            var taskCount = State.getTasks.Count;
            var orderCount = State.ordersPile.Count;
            WriteLine($"got {taskCount} tasks and {orderCount} orders");
        }
        public static void GetOrderBooks()
        {
            var count = State.ordersPile.Count;
            WriteLine($"got {count} orders");
            PrintElapsed();
        }
        public static void Sorting()
        {
            //
            PrintElapsed();
        }
        public static void Arbitrages()
        {
            //
            PrintElapsed();
        }
        public static void PrintElapsed()
        {
            var elapsed = Utilities.Chronometer.ElapsedMilliseconds;
            WriteLine($"took {elapsed} ms up to this point");
        }
    }
}