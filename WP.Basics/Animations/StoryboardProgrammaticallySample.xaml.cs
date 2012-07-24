using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WP.Basics.Animations
{
    public partial class StoryboardProgrammaticallySample : UserControl
    {
        public StoryboardProgrammaticallySample()
        {
            InitializeComponent();

            Rectangle rect = new Rectangle
            {
                Fill = new SolidColorBrush(Colors.Green),
                RenderTransform = new TranslateTransform()
            };
            LayoutRoot.Children.Add(rect);

            Loaded += (s, e) =>
                          {
                              var sb = CreateAndApplyStoryboard(rect); 
                              sb.Begin();
                          };
        }

        public Storyboard CreateAndApplyStoryboard(UIElement targetElement)
        {
            Storyboard sb = new Storyboard();

            DoubleAnimation animation = new DoubleAnimation { From = 0, To = 700 };

            Storyboard.SetTarget(animation, targetElement);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));

            sb.Children.Add(animation);
            return sb;
        }
    }
}
