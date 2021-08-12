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
            Configuration.Configure();
            if (Configuration.AutoRefresh == true)
            {
                while (true)
                {
                    await Refresh();
                    await Task.Delay(Configuration.RefreshInterval);
                }
            }
            else
            {
                while (true)
                {
                    await Refresh();
                    ReadLine();
                }
            }
        }
        
        static async Task Refresh()
        {
            Reset();
            if (Configuration.DiagMode == true)
            {
                Diagnostics.Reset();
            }
            await GetOrderBooks();
            if (Configuration.DiagMode == true)
            {
                Diagnostics.OrderBooks();
            }
        }
        static void Reset()
        {
            Utilities.Chronometer.Restart();
            State.GetTasks.Clear();
            State.OrdersPile.Clear();
            State.Opportunuties.Clear();
        }
        static async Task GetOrderBooks()
        {
            Binance.RunGetTask();
            Bitci.RunGetTask();
            BtcTurk.RunGetTask();
            Paribu.RunGetTask();
            await Task.WhenAll(State.GetTasks);
        }
    }
}