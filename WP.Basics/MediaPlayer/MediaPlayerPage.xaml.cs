using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WP.Basics.MediaPlayer
{
    public partial class MediaPlayerPage : PhoneApplicationPage
    {
        private DispatcherTimer currentPosition = new DispatcherTimer();

        public MediaPlayerPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            myMediaElement.MediaOpened += new RoutedEventHandler(myMediaElement_MediaOpened);
            myMediaElement.MediaEnded += new RoutedEventHandler(myMediaElement_MediaEnded);
            myMediaElement.CurrentStateChanged += new RoutedEventHandler(myMediaElement_CurrentStateChanged);
            currentPosition.Tick += new EventHandler(currentPosition_Tick);

            myMediaElement.Source = new Uri("http://ecn.channel9.msdn.com/o9/ch9/4807/574807/ISWPE05SLToolKitForWP_ch9.wmv", UriKind.Absolute);
        }

        private void myMediaElement_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            if (myMediaElement.CurrentState == MediaElementState.Playing)
            {
                currentPosition.Start();
                ((ApplicationBarIconButton) ApplicationBar.Buttons[0]).IsEnabled = false; // play
                ((ApplicationBarIconButton) ApplicationBar.Buttons[1]).IsEnabled = true; // pause
                ((ApplicationBarIconButton) ApplicationBar.Buttons[2]).IsEnabled = true; // stop
            }
            else if (myMediaElement.CurrentState == MediaElementState.Paused)
            {
                currentPosition.Stop();
                ((ApplicationBarIconButton) ApplicationBar.Buttons[0]).IsEnabled = true; // play
                ((ApplicationBarIconButton) ApplicationBar.Buttons[1]).IsEnabled = false; // pause
                ((ApplicationBarIconButton) ApplicationBar.Buttons[2]).IsEnabled = true; // stop
            }
            else
            {
                currentPosition.Stop();
                ((ApplicationBarIconButton) ApplicationBar.Buttons[0]).IsEnabled = true; // play
                ((ApplicationBarIconButton) ApplicationBar.Buttons[1]).IsEnabled = false; // pause
                ((ApplicationBarIconButton) ApplicationBar.Buttons[2]).IsEnabled = false; // stop
            }
        }

        private void myMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            myMediaElement.Stop();
        }

        private void myMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            pbVideo.Maximum = (int) myMediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            myMediaElement.Play();
        }

        private void currentPosition_Tick(object sender, EventArgs e)
        {
            pbVideo.Value = (int) myMediaElement.Position.TotalMilliseconds;
        }

        private void PlayClick(object sender, EventArgs e)
        {
            myMediaElement.Play();
        }

        private void PauseClick(object sender, EventArgs e)
        {
            myMediaElement.Pause();
        }

        private void StopClick(object sender, EventArgs e)
        {
            myMediaElement.Stop();
        }
    }
}