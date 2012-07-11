using System;
using Microsoft.Phone.Controls;

namespace WP.Sensors
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BiGPSClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/GPSSamplePage.xaml", UriKind.Relative));
        }

        private void BiAccelerometer(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AccelerometerSamplePage.xaml", UriKind.Relative));
        }

        private void BiCompass(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CompassSamplePage.xaml", UriKind.Relative));
        }

        private void BiGyroscope(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/GyroscopeSamplePage.xaml", UriKind.Relative));
        }

        private void biMotion(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MotionSamplePage.xaml", UriKind.Relative));
        }
    }
}