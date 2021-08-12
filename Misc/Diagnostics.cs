using static System.Console;

namespace KriptoArbitraj
{
    static class Diagnostics
    {
        public static void Configure()
        {
            WriteLine("After Configure():");
            WriteLine($"pair: {Configuration.Pair}, depth: {Configuration.Depth}");
            WriteLine($"minEpsion: {Configuration.MinEpsilon}, minVolume: {Configuration.MinVolume}, minProfit: {Configuration.MinProfit}");
            Write($"autoRefresh = {Configuration.AutoRefresh}");
            if(Configuration.AutoRefresh == true)
            {
                WriteLine($", interval: {Configuration.RefreshInterval}");
            }
            else
            {
                WriteLine();
            }
            WriteLine($"diagMode: {Configuration.DiagMode}");
        }
        public static void Reset()
        {
            WriteLine("After Reset():");
            var taskCount = State.GetTasks.Count;
            var orderCount = State.OrdersPile.Count;
            WriteLine($"got {taskCount} tasks and {orderCount} orders");
        }
        public static void GetOrderBooks()
        {
            WriteLine("After GetOrderBooks():");
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