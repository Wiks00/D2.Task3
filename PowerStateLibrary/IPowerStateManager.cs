using System;
using System.Runtime.InteropServices;

namespace PowerStateLibrary
{
    [ComVisible(true)]
    [Guid("BB52BF7A-3EFD-4C23-B7D6-F56A53CF1C7D")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IPowerStateManager
    {
        [DispId(1)]
        string GetPowerInformation();

        [DispId(2)]
        string GetBatteryState();

        [DispId(3)]
        string GetLastWakeTime();

        [DispId(4)]
        string GetLastSleepTime();
    
        [DispId(5)]
        void Hibernate();

        [DispId(6)]
        void Standby();

        [DispId(7)]
        void ReserveHibernationFile();

        [DispId(8)]
        void DeleteHibernationFile();
    }
}