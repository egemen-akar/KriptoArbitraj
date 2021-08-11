using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace KriptoArbitraj
{
    static class State
    {
        public static List<Task> getTasks = new();
        public static ConcurrentBag<Order> ordersPile = new();
    }
}


