using System;
using System.Globalization;
using System.Threading.Tasks;

using static System.Console;

namespace KriptoArbitraj
{
    static class Program
    {
        static async Task Main()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            while(true)
            {
                WriteLine("awaiting refresh");
                try
                {
                    await Refresh();
                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                }
                WriteLine("done");
                ReadLine();
            }
        }

        static async Task Refresh()
        {
            Utilities.Chronometer.Restart();
            State.orders.Clear();
            State.getTasks.Clear();
            State.getTasks.Add(Binance.GetOrdersAsync());
            State.getTasks.Add(Bitci.GetOrdersAsync());
            State.getTasks.Add(BtcTurk.GetOrdersAsync());
            State.getTasks.Add(Paribu.GetOrdersAsync());
            await Task.WhenAll(State.getTasks);
            
            if(Configuration.DiagMode == true)
            {
                Tests.PrintOrderBooks();
            }
        }
    }
}
