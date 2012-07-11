using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Threading;

namespace WP.Basics.Profiling
{
    public static class MemoryProfiler
    {
        private static readonly TimeSpan Interval = TimeSpan.FromSeconds(2);

        private static DispatcherTimer timer = null;

        public static void Start()
        {
            if(timer == null)
            {
                timer = new DispatcherTimer() { Interval = Interval };
                timer.Tick += TimerTick;
            }
            timer.Start();
        }

        public static void Stop()
        {
            if(timer != null)
            {
                if(timer.IsEnabled)
                {
                    timer.Stop();
                }
                timer = null;
            }
        }

        private static void TimerTick(object sender, EventArgs e)
        {
            Log("Current Memory Usage", Microsoft.Phone.Info.DeviceStatus.ApplicationCurrentMemoryUsage.ToString(CultureInfo.InvariantCulture));
            Log("Peak Memory Usage", Microsoft.Phone.Info.DeviceStatus.ApplicationPeakMemoryUsage.ToString(CultureInfo.InvariantCulture));

        }

        private static void Log(string parameterName, string parameterValue)
        {
            Debug.WriteLine(string.Format("{0}: {1}", parameterName, parameterValue));
        }
    }
}
