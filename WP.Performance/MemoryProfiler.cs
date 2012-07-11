using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Phone.Shell;

namespace WP.Performance
{
    public static class MemoryProfiler
    {
        private static bool isConsoleEnabled;
        private static bool isPopupEnabled;

        private static DispatcherTimer timer;
        private static TimeSpan timerInterval;

        private static Popup popupControl;
        private static StackPanel popupContentPanel;
        private static TextBlock textblockCurrentMemoryUsage;
        private static TextBlock textblockPeakMemoryUsage;

        public static void Start()
        {
            Start(true, false);
        }

        public static void Start(bool isConsoleLoggingEnabled, bool isPopupLoggingEnabled)
        {
            Start(isConsoleLoggingEnabled, isPopupLoggingEnabled, TimeSpan.FromMilliseconds(1000));
        }

        public static void Start(bool isConsoleLoggingEnabled, bool isPopupLoggingEnabled, TimeSpan interval)
        {
            isConsoleEnabled = isConsoleLoggingEnabled;
            isPopupEnabled = isPopupLoggingEnabled;
            timerInterval = interval;

            if (isPopupEnabled)
            {
                CreateAndDisplayPopup();
            }

            if (timer == null)
            {
                timer = new DispatcherTimer() { Interval = timerInterval };
                timer.Tick += TimerTick;
            }
            timer.Start();
        }

        public static void Stop()
        {
            if (timer != null)
            {
                timer.Tick -= TimerTick;
                if (timer.IsEnabled)
                {
                    timer.Stop();
                }
                timer = null;
            }

            if (popupControl != null)
            {
                popupControl.IsOpen = false;
            }
        }

        private static void CreateAndDisplayPopup()
        {
            popupControl = new Popup();

            popupContentPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            popupContentPanel.Children.Add(GenerateTextBlockForPopup(Colors.Orange, "Current: "));
            textblockCurrentMemoryUsage = GenerateTextBlockForPopup(Colors.Orange, "N/A");
            popupContentPanel.Children.Add(textblockCurrentMemoryUsage);

            popupContentPanel.Children.Add(GenerateTextBlockForPopup(Colors.Red, "Peak: "));
            textblockPeakMemoryUsage = GenerateTextBlockForPopup(Colors.Red, "N/A");
            popupContentPanel.Children.Add(textblockPeakMemoryUsage);

            popupControl.Child = popupContentPanel;

            popupControl.IsOpen = true;
        }

        private static TextBlock GenerateTextBlockForPopup(Color foregroundColor, string text)
        {
            var tb = new TextBlock();
            tb.FontSize = 11;
            tb.Text = text;
            tb.Foreground = new SolidColorBrush(foregroundColor);
            tb.Margin = new Thickness(5, 0, 0, 0);
            return tb;
        }

        private static void TimerTick(object sender, EventArgs e)
        {
            string currentMemory = GetFormatedMemoryUsage(Microsoft.Phone.Info.DeviceStatus.ApplicationCurrentMemoryUsage);
            string peakMemory = GetFormatedMemoryUsage(Microsoft.Phone.Info.DeviceStatus.ApplicationPeakMemoryUsage);

            if (isConsoleEnabled)
            {
                LogToConsole("Current Memory Usage", currentMemory);
                LogToConsole("Peak Memory Usage", peakMemory);
            }

            if (isPopupEnabled)
            {
                popupContentPanel.Margin = (SystemTray.IsVisible) ? new Thickness(5, 30, 0, 0) : new Thickness(5, 0, 0, 0);
                textblockCurrentMemoryUsage.Text = currentMemory;
                textblockPeakMemoryUsage.Text = peakMemory;
            }
        }

        private static void LogToConsole(string parameterName, string parameterValue)
        {
            Debug.WriteLine(string.Format("{0}: {1}", parameterName, parameterValue));
        }

        private static string GetFormatedMemoryUsage(long memoryUsageInBytes)
        {
            return string.Format("{0:N}", memoryUsageInBytes / 1024);
        }
    }
}
