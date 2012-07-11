using System;
using Microsoft.Phone.Controls;

namespace WP.Basics.TouchSamples
{
    public partial class ManipulationSamples : PhoneApplicationPage
    {
        public ManipulationSamples()
        {
            InitializeComponent();
        }

        private void GoToManipulation(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/TouchSamples/ManipulationEventsSample.xaml", UriKind.Relative));
        }

        private void GoToTouch(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/TouchSamples/MouseEventsSample.xaml", UriKind.Relative));
        }

        private void GoToMouse(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/TouchSamples/TouchEventsSample.xaml", UriKind.Relative));
        }
    }
}