using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace WP.Services
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void MicrosoftTagServiceClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/MicrosoftTagServicePage.xaml", UriKind.Relative));
        }

        private void RSSReaderClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/RSSReaderPage.xaml", UriKind.Relative));
        }
    }
}