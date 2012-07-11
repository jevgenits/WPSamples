using System;
using Microsoft.Phone.Controls;

namespace WP.Basics.Navigation
{
    public partial class NavigationPage1 : PhoneApplicationPage
    {
        public NavigationPage1()
        {
            InitializeComponent();
        }

        private void GoToPage2(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new Uri(string.Format("/Navigation/NavigationPage2.xaml?p={0}",
                    Uri.EscapeDataString(tbText.Text.Trim())), UriKind.Relative)
                    );
        }
    }
}