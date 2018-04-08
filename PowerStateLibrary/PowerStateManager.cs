using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PowerStateLibrary
{
    [ComVisible(true)]
    [Guid("AA8CECE2-4AF9-4410-962B-27BBC9884522")]
    [ClassInterface(ClassInterfaceType.None)]
    public class PowerStateManager : IPowerStateManager
    {
        public PowerStateManager()
        {
        }

        public string GetPowerInformation()
            => PowerState.GetPowerInformation();

        public string GetBatteryState()
            => PowerState.GetBatteryState();

        public string GetLastWakeTime()
            => PowerState.GetLastWakeTime();

        public string GetLastSleepTime()
            => PowerState.GetLastSleepTime();

        public void Hibernate()
            => PowerState.Hibernate();

        public void Standby()
            => PowerState.Standby();

        public void ReserveHibernationFile()
            => PowerState.ReserveHibernationFile();

        public void DeleteHibernationFile()
            => PowerState.DeleteHibernationFile();
    }
}
