using Microsoft.Phone.Marketplace;

namespace WP.Basics.License
{
    public class LicenseHelper
    {
        private const bool IsEmulatorInTrial = false;

        public bool IsTrial
        {
            get
            {
                if (Microsoft.Devices.Environment.DeviceType == Microsoft.Devices.DeviceType.Emulator)
                {
                    return IsEmulatorInTrial;
                }
                return new LicenseInformation().IsTrial();
            }
        }
    }
}
