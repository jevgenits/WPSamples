using System;
using WP.Common.XmlReaderSample;
using Microsoft.Phone.Controls;

namespace WP.Basics
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            XmlParser.CompareParsers();
        }

        private void GoToNavigation(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Navigation/NavigationPage1.xaml", UriKind.Relative));
        }

        private void GoToStoringData(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/DataStoring/DataStoringSample.xaml", UriKind.Relative));
        }

        private void GoToLaunchers(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Launchers/LaunchersSamplePage.xaml", UriKind.Relative));
        }

        private void GoToChoosers(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Choosers/ChoosersSamplePage.xaml", UriKind.Relative));
        }

        private void GoToLiveTiles(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/LiveTiles/LiveTileUpdateSample.xaml", UriKind.Relative));
        }

        private void GoToTouchSamples(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/TouchSamples/ManipulationSamples.xaml", UriKind.Relative));
        }

        private void GoToInputScopes(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/InputScopes/InputScopesPage.xaml", UriKind.Relative));
        }

    }
}