using System.Windows;
using Microsoft.Devices.Sensors;
using Microsoft.Phone.Controls;

namespace WP.Sensors
{
    public partial class MotionSamplePage : PhoneApplicationPage
    {
        private Motion _m;

        public MotionSamplePage()
        {
            InitializeComponent();

            Loaded += (s, e) =>
                          {
                              if (Motion.IsSupported)
                              {
                                  _m = new Motion();
                                  _m.CurrentValueChanged += MotionCurrentValueChanged;
                                  _m.Start();
                              }
                          };
        }

        void MotionCurrentValueChanged(object sender, SensorReadingEventArgs<MotionReading> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => 
                {
                    tbYaw.Text = e.SensorReading.Attitude.Yaw.ToString("0");
                    tbPitch.Text = e.SensorReading.Attitude.Pitch.ToString("0");
                    tbRoll.Text = e.SensorReading.Attitude.Roll.ToString("0");

                    tbAXCoordinate.Text =
                        e.SensorReading.DeviceAcceleration.X.ToString("0.000");
                    tbAYCoordinate.Text =
                        e.SensorReading.DeviceAcceleration.Y.ToString("0.000");
                    tbAZCoordinate.Text =
                        e.SensorReading.DeviceAcceleration.Z.ToString("0.000");

                    tbXCoordinate.Text =
                        e.SensorReading.DeviceRotationRate.X.ToString("0.000");
                    tbYCoordinate.Text =
                        e.SensorReading.DeviceRotationRate.Y.ToString("0.000");
                    tbZCoordinate.Text =
                        e.SensorReading.DeviceRotationRate.Z.ToString("0.000");
                });
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (_m != null)
            {
                _m.Stop();
                _m.CurrentValueChanged -= MotionCurrentValueChanged;
            }
        }
    }
}