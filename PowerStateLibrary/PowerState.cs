using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PowerStateLibrary
{
    public static class PowerState
    {
        private const uint STATUS_SUCCESS = 0;
        private const long divider = 10000000;

        public static string GetPowerInformation()
        {
            var spi = Wrapper<SystemPowerInformation>(PowerIinformationLevel.SystemPowerInformation);

            return $"Idleness: {spi.Idleness}";
        }

        public static string GetBatteryState()
        {
            var sbs = Wrapper<SystemBatteryState>(PowerIinformationLevel.SystemBatteryState);

            var state = sbs.AcOnLine
                ? "on powerline"
                : sbs.Charging
                    ? "charging"
                    : $"discharging, estimated time {new TimeSpan(0, 0, (int)sbs.EstimatedTime)}";

            return $"Battery is {state}";
        }

        public static string GetLastWakeTime()
            => GetLastBootTime().AddSeconds(Wrapper<long>(PowerIinformationLevel.LastWakeTime) / divider).ToString();

        public static string GetLastSleepTime()
            => GetLastBootTime().AddSeconds(Wrapper<long>(PowerIinformationLevel.LastSleepTime) / divider).ToString();

        public static void Hibernate()
            => SetSuspendState(true, true, true);

        public static void Standby()
            => SetSuspendState(false, true, true);

        public static void ReserveHibernationFile()
            => HibernationFile(true);

        public static void DeleteHibernationFile()
            => HibernationFile(false);

        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern bool SetSuspendState(
            bool Hibernate,
            bool ForceCritical,
            bool DisableWakeEvent
        );

        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern uint CallNtPowerInformation(
            PowerIinformationLevel InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            IntPtr result,
            int nOutputBufferSize
        );

        private static T Wrapper<T>(PowerIinformationLevel informationLevel)
        {
            int size = Marshal.SizeOf<T>();
            IntPtr typePtr = Marshal.AllocCoTaskMem(size);

            uint status = CallNtPowerInformation(informationLevel, IntPtr.Zero, 0, typePtr, size);
            if (status != STATUS_SUCCESS)
                return default(T);

            return Marshal.PtrToStructure<T>(typePtr);
        }

        private static void HibernationFile(bool logic)
        {
            int hiberParam = logic ? 1 : 0;
            var pointer = Marshal.AllocHGlobal(sizeof(int));
            Marshal.WriteInt32(pointer, hiberParam);

            CallNtPowerInformation(
                PowerIinformationLevel.SystemReserveHiberFile,
                pointer,
                Marshal.SizeOf(typeof(int)),
                IntPtr.Zero, 
                0);

            Marshal.FreeHGlobal(pointer);
        }

        private static DateTime GetLastBootTime()
        {
            SelectQuery query =
                new SelectQuery(@"SELECT LastBootUpTime FROM Win32_OperatingSystem WHERE Primary='true'");

            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(query);

            DateTime bootTime = new DateTime();
            foreach (ManagementObject mo in searcher.Get().OfType<ManagementObject>())
            {
                bootTime =
                    ManagementDateTimeConverter.ToDateTime(
                        mo.Properties["LastBootUpTime"].Value.ToString());
            }

            return bootTime;
        }

        private enum PowerIinformationLevel : uint
        {
            LastSleepTime = 15,
            LastWakeTime = 14,
            SystemBatteryState = 5,
            SystemReserveHiberFile = 10,
            SystemPowerInformation = 12
        }
    }
}
