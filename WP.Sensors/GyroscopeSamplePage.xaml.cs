using System.Windows;
using Microsoft.Devices.Sensors;
using Microsoft.Phone.Controls;

namespace WP.Sensors
{
    public partial class GyroscopeSamplePage : PhoneApplicationPage
    {

        Gyroscope _g;

        public GyroscopeSamplePage()
        {
            InitializeComponent();

            Loaded += (s, e) =>
                          {
                              if (Gyroscope.IsSupported)
                              {
                                  _g = new Gyroscope();
                                  _g.CurrentValueChanged += GyroscopeCurrentValueChanged;
                                  _g.Start();
                              }
                          };
        }

        void GyroscopeCurrentValueChanged(object sender, SensorReadingEventArgs<GyroscopeReading> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    tbXCoordinate.Text = e.SensorReading.RotationRate.X.ToString();
                    tbYCoordinate.Text = e.SensorReading.RotationRate.Y.ToString();
                    tbZCoordinate.Text = e.SensorReading.RotationRate.Z.ToString();
                });

        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (_g != null)
            {
                _g.Stop();
                _g.CurrentValueChanged -= GyroscopeCurrentValueChanged;
            }
        }
    }
}