using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Phone.Controls;

namespace WP.Basics.TouchSamples
{
    public partial class ManipulationEventsSample : PhoneApplicationPage
    {
        private readonly TransformGroup _transformGroup;
        private readonly TranslateTransform _translation;

        // ManipulationStarted Event - Occurs when an input device begins a manipulation on the UIElement.
        // ManipulationDelta Event - Occurs when the input device changes position during a manipulation.
        // ManipulationCompleted Event - Occurs when a manipulation and inertia on the UIElement are complete.
        // ManipulationStartedEventArgs - Provides data for the ManipulationStarted event.
        // ManipulationDeltaEventArgs - Provides data for the ManipulationDelta event.
        // ManipulationVelocities - Describes the speed at which manipulations occur.
        // ManipulationCompletedEventArgs - Provides data for the ManipulationCompleted event.

        public ManipulationEventsSample()
        {
            InitializeComponent();

            _transformGroup = new TransformGroup();
            _translation = new TranslateTransform();

            _transformGroup.Children.Add(this._translation);
            rect.RenderTransform = _transformGroup;

            // manipulation events
            ManipulationStarted += PageManipulationStarted;
            ManipulationDelta += PageManipulationDelta;
            ManipulationCompleted += PageManipulationCompleted;

        }

        void PageManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            
        }

        void PageManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            _translation.X += e.DeltaManipulation.Translation.X;
            _translation.Y += e.DeltaManipulation.Translation.Y;
        }

        void PageManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            
        }

        
    }
}