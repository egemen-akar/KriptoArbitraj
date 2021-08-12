using System.Collections.Generic;
using System.Threading.Tasks;

namespace KriptoArbitraj
{
    static class State
    {
        public static List<Task<List<Order>>> GetTasks = new();
        public static List<Order> OrdersPile = new();
        public static List<Arbitrage> Opportunuties = new();
    }
}


