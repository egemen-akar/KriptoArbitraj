using static System.Console;

namespace KriptoArbitraj
{
    static class Diagnostics
    {
        public static void Reset()
        {
            var taskCount = State.GetTasks.Count;
            var orderCount = State.OrdersPile.Count;
            WriteLine($"got {taskCount} tasks and {orderCount} orders");
        }
        public static void OrderBooks()
        {
            var count = State.OrdersPile.Count;
            WriteLine($"got {count} orders");
            PrintElapsed();
        }
        public static void Sort()
        {
            //
            PrintElapsed();
        }
        public static void Arbitrage()
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