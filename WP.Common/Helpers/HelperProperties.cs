using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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
                if (Microsoft.Devices.Environment.DeviceType
                       == Microsoft.Devices.DeviceType.Emulator)
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
