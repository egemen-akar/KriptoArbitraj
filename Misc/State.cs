using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace KriptoArbitraj
{
    static class State
    {
        public static List<Task> GetTasks = new();
        public static ConcurrentBag<Order> OrdersPile = new();
        public static List<Arbitrage> Opportunuties = new();
    }
}


