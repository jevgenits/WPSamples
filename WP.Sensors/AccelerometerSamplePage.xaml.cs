using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Devices.Sensors;

namespace WP.Sensors
{
    public partial class AccelerometerSamplePage : PhoneApplicationPage
    {
        readonly Accelerometer _acc = new Accelerometer();

        public AccelerometerSamplePage()
        {
            InitializeComponent();

            _acc.CurrentValueChanged += AccelerometerCurrentValueChanged;
            _acc.Start();
        }

        void AccelerometerCurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            // 50 times per second
            // Dispatcher.BeginInvoke( Action ); to call UI Thread
            // The Dispatcher class guarantees that this 
            // code will be executed on the UI thread.

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                // Thread-safe
                tbXCoordinate.Text = e.SensorReading.Acceleration.X.ToString("0.000");
                tbYCoordinate.Text = e.SensorReading.Acceleration.Y.ToString("0.000");
                tbZCoordinate.Text = e.SensorReading.Acceleration.Z.ToString("0.000");
            });
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (_acc.State != SensorState.Disabled)
            {
                _acc.Stop();
                _acc.CurrentValueChanged -= AccelerometerCurrentValueChanged;
            }
        }

    }
}