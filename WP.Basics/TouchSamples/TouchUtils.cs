using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WP.Basics.TouchSamples
{
    public class TouchUtils
    {
        public static Ellipse CreateEllipse(Point p)
        {
            var el = new Ellipse()
            {
                Fill = new SolidColorBrush(Colors.Blue),
                Opacity = 0.6,
                Height = 50,
                Width = 50
            };
            Canvas.SetLeft(el, p.X - 25);
            Canvas.SetTop(el, p.Y - 25);  
            return el;
        }


        public static Path CreateLinearPath(Point startPoint, Point endPoint)
        {

            var linearPath = new Path();
            linearPath.Stroke = new SolidColorBrush(Colors.Green);
            linearPath.StrokeThickness = 10;

            var linearPathGeometry = new PathGeometry();
            var linearPathFigure = new PathFigure();

            linearPathFigure.IsClosed = false;

            linearPathFigure.StartPoint = startPoint;

            var newSegment = new LineSegment();
            newSegment.Point = endPoint;

            linearPathFigure.Segments = new PathSegmentCollection() { newSegment };
            linearPathGeometry.Figures.Add(linearPathFigure);
            linearPath.Data = linearPathGeometry;

            return linearPath;
        }

    }
}
