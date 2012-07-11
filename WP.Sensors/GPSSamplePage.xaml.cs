using System;
using Microsoft.Phone.Controls;
using System.Device.Location;
using Microsoft.Phone.Reactive;

namespace WP.Sensors
{
    public partial class GpsSamplePage : PhoneApplicationPage
    {
        private readonly GeoCoordinateWatcher _gcw = new GeoCoordinateWatcher();

        private readonly Random _random = new Random();

        public GpsSamplePage()
        {
            InitializeComponent();

            bool isSimulation = false;
            if (!isSimulation)
            {
                // standard location service
                _gcw.PositionChanged += (s, e) => GcwPositionChanged(e);
                _gcw.Start();
            }
            else
            {
                // simulated location change events
                Observable.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(4))
                .Select(x => 
                    new GeoPositionChangedEventArgs<GeoCoordinate>(
                        new GeoPosition<GeoCoordinate>(DateTime.Now,
                            new GeoCoordinate()
                                {
                                    // -90 <= latitude <= 90
                                    Latitude = (_random.NextDouble() * 180.0) - 90.0,
                                    // -180 <= longitude <= 180
                                    Longitude = (_random.NextDouble() * 360.0) - 180.0
                                }

                        ))).ObserveOnDispatcher().Subscribe(GcwPositionChanged);
            }
        }

        void GcwPositionChanged(GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                // thread safe
                tbLatitude.Text = e.Position.Location.Latitude.ToString();
                tbLongitude.Text = e.Position.Location.Longitude.ToString();
            });
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (_gcw.Status != GeoPositionStatus.Disabled)
                _gcw.Stop();
        }
    }
}