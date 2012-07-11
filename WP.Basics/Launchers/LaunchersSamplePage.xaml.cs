using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace WP.Basics.Launchers
{
    public partial class LaunchersSamplePage : PhoneApplicationPage
    {
        public LaunchersSamplePage()
        {
            InitializeComponent();
        }

        private void LaunchWebBrowserTaks(object sender, RoutedEventArgs e)
        {
            var wbTask = new WebBrowserTask();
            wbTask.Uri = new Uri("http://www.EUGENEDOTNET", UriKind.Absolute);
            wbTask.Show();
        }
    }
}