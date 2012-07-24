using Microsoft.Phone.Info;
using Microsoft.Phone.Marketplace;

namespace WP.Common.Helpers
{
    public class HelperProperties
    {
        private const bool emulatorIsTrial = false;

        public bool IsTrialMode
        {
            get
            {
                if (Microsoft.Devices.Environment.DeviceType == Microsoft.Devices.DeviceType.Emulator)
                {
                    return emulatorIsTrial;
                }
                return (new LicenseInformation().IsTrial());
            }
        }

        private const bool emulatorIsLowMemory = false;

        public bool IsLowMemoryDevice
        {
            get
            {
                if (Microsoft.Devices.Environment.DeviceType
                    == Microsoft.Devices.DeviceType.Emulator)
                {
                    return emulatorIsLowMemory;
                }
                else
                {
                    return DeviceStatus.DeviceTotalMemory <= 268435456;
                }
            }
        }
    }
}
