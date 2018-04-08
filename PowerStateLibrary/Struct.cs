using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PowerStateLibrary
{
    public struct SystemPowerInformation
    {
        public uint MaxIdlenessAllowed;

        public uint Idleness;

        public uint TimeRemaining;

        public bool CoolingMode;
    }

    public struct SystemBatteryState
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool AcOnLine;

        [MarshalAs(UnmanagedType.I1)]
        public bool BatteryPresent;

        [MarshalAs(UnmanagedType.I1)]
        public bool Charging;

        [MarshalAs(UnmanagedType.I1)]
        public bool Discharging;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
        public bool[] Spare1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
        public bool[] Spare2;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
        public bool[] Spare3;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
        public bool[] Spare4;

        public uint MaxCapacity;

        public uint RemainingCapacity;

        public uint Rate;

        public uint EstimatedTime;

        public uint DefaultAlert1;

        public uint DefaultAlert2;
    }
}
