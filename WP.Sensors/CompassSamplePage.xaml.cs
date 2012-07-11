using System.Windows;
using Microsoft.Devices.Sensors;
using Microsoft.Phone.Controls;

namespace WP.Sensors
{
    public partial class CompassSamplePage : PhoneApplicationPage
    {
        readonly Compass _c;
        
        public CompassSamplePage()
        {
            InitializeComponent();

            // not all the devices support compass

            if(Compass.IsSupported)
            {
                _c = new Compass();
                _c.CurrentValueChanged += CompassCurrentValueChanged;
                _c.Start();
            }
        }

        void CompassCurrentValueChanged(object sender, SensorReadingEventArgs<CompassReading> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => { 
                tbMagneticHeading.Text = e.SensorReading.MagneticHeading.ToString();
            });
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (_c != null)
            {
                _c.Stop();
                _c.CurrentValueChanged -= CompassCurrentValueChanged;
            }
        }
    }
}