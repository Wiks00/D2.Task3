using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PowerStateLibrary;

namespace Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = PowerState.GetPowerInformation();
            var result1 = PowerState.GetBatteryState();
            var result2 = PowerState.GetLastSleepTime();
            var result3 = PowerState.GetLastWakeTime();

            ReadKey();
        }
    }
}
