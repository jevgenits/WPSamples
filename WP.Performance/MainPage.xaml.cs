using System;
using System.Windows;
using WP.Performance.BasicFrameworkTests;
using Microsoft.Phone.Controls;

namespace WP.Performance
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += (s, e) => BasicFrameworkStressTests.ExecuteTests();
        }

        private void ToggleFrameRateCounters(object sender, RoutedEventArgs e)
        {
            if (CheckBoxToggleFrameRateCounters.IsChecked.HasValue)
            {
                Application.Current.Host.Settings.EnableFrameRateCounter = CheckBoxToggleFrameRateCounters.IsChecked.Value;
            }
        }

        private void ToggleEnableRedrawRegions(object sender, RoutedEventArgs e)
        {
            if (CheckBoxToggleEnableRedrawRegions.IsChecked.HasValue)
            {
                Application.Current.Host.Settings.EnableRedrawRegions = CheckBoxToggleEnableRedrawRegions.IsChecked.Value;
            }
        }

        private void ToggleMemoryProfiler(object sender, RoutedEventArgs e)
        {
            if (CheckBoxToggleMemoryProfiler.IsChecked.HasValue && CheckBoxToggleMemoryProfiler.IsChecked.Value)
            {
                MemoryProfiler.Start(true, true);
            }
            else
            {
                MemoryProfiler.Stop();
            }
        }

        private void OpenFillRate(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/TestPages/FillRateTestPage.xaml", UriKind.Relative));
        }

        private void OpenAnimations(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/TestPages/AnimationsTestPage.xaml", UriKind.Relative));
        }

        private void OpenLeakingPage(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/TestPages/LeakingPage.xaml", UriKind.Relative));
        }
    }
}