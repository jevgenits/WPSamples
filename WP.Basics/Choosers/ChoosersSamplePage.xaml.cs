using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace WP.Basics.Choosers
{
    public partial class ChoosersSamplePage : PhoneApplicationPage
    {
        public ChoosersSamplePage()
        {
            InitializeComponent();
        }

        private void LaunchCameraCapture(object sender, RoutedEventArgs e)
        {
            var cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed += CameraCaptureTaskCompleted;
            try
            {
                cameraCaptureTask.Show();
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("An error occurred.");
            }
        }

        void CameraCaptureTaskCompleted(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                var bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                imgFromCamera.Source = bmp;
            }
        }

    }
}