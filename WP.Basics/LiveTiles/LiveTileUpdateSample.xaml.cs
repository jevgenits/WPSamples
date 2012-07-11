using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;

namespace WP.Basics.LiveTiles
{
    public partial class LiveTileUpdateSample : PhoneApplicationPage
    {

        PeriodicTask _periodicTask;

        private const string PeriodicTaskName = "LiveTileUpdateScheduledTask";

        public LiveTileUpdateSample()
        {
            InitializeComponent();
        }

        private void EnableLiveTileUpdateTask(object sender, RoutedEventArgs e)
        {
            _periodicTask = ScheduledActionService.Find(PeriodicTaskName) as PeriodicTask;

            if (_periodicTask != null)
            {
                RemoveAgent(PeriodicTaskName);
            }

            _periodicTask = new PeriodicTask(PeriodicTaskName);
            
            _periodicTask.Description = "eugenedotnet live tile update task";

            try
            {
                ScheduledActionService.Add(_periodicTask);
                
                #if DEBUG
                ScheduledActionService.LaunchForTest(PeriodicTaskName, TimeSpan.FromSeconds(30));
                #endif

                btnDisableLiveTileUpdate.IsEnabled = true;
                btnEnableLiveTileUpdate.IsEnabled = false;
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                }
            }

        }

        private void DisableLiveTileUpdateTask(object sender, RoutedEventArgs e)
        {
            RemoveAgent(PeriodicTaskName);
        }


        private void RemoveAgent(string name)
        {
            try
            {
                ScheduledActionService.Remove(name);
                btnDisableLiveTileUpdate.IsEnabled = false;
                btnEnableLiveTileUpdate.IsEnabled = true;
            }
            catch (Exception)
            {
                // agent was probably not found
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            _periodicTask = ScheduledActionService.Find(PeriodicTaskName) as PeriodicTask;

            btnDisableLiveTileUpdate.IsEnabled = (_periodicTask != null);
            btnEnableLiveTileUpdate.IsEnabled = (_periodicTask == null);
        }

    }
}