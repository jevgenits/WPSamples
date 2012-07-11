using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Phone.Controls;

namespace WP.Basics.TouchSamples
{
    public partial class MouseEventsSample : PhoneApplicationPage
    {

        private readonly TransformGroup _transformGroup;
        private readonly TranslateTransform _translation;

        public MouseEventsSample()
        {
            InitializeComponent();

            _transformGroup = new TransformGroup();
            _translation = new TranslateTransform();

            _transformGroup.Children.Add(_translation);
            rect.RenderTransform = _transformGroup;

            // mouse events
            MouseLeftButtonDown += PageMouseLeftButtonDown;
            MouseMove += PageMouseMove;
            MouseLeftButtonUp += PageMouseLeftButtonUp;
            MouseLeave += PageMouseLeave;
        }

        void PageMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        void PageMouseMove(object sender, MouseEventArgs e)
        {
            _translation.X = e.GetPosition(ContentCanvas).X - 50;
            _translation.Y = e.GetPosition(ContentCanvas).Y - 50;
    
        }

        void PageMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        void PageMouseLeave(object sender, MouseEventArgs e)
        {
            
        }
        
    }
}