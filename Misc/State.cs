using System.Collections.Generic;
using System.Threading.Tasks;

namespace KriptoArbitraj
{
    static class State
    {
        public static List<Task> getTasks = new();
        public static List<Order> orders = new();
    }
}


