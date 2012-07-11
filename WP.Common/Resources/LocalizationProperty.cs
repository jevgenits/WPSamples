using System.Windows;
using System.Windows.Controls;

namespace WP.Common.Resources
{
    public class LocalizationProperty
    {

        #region Key (Attached DependencyProperty)

        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.RegisterAttached("Key", typeof(string), typeof(LocalizationProperty), new PropertyMetadata(new PropertyChangedCallback(OnKeyChanged)));

        public static void SetKey(DependencyObject o, string value)
        {
            o.SetValue(KeyProperty, value);
        }

        public static string GetKey(DependencyObject o)
        {
            return (string)o.GetValue(KeyProperty);
        }

        private static void OnKeyChanged(DependencyObject depObject, DependencyPropertyChangedEventArgs e)
        {
            var key = e.NewValue as string;
             
            if (key == null)
                return;

            if (depObject is TextBlock)
            {
                var sender = (TextBlock)depObject;
                sender.Text = Localization.GetByLocalizationKey(key);
            }
            else if (depObject is ContentControl)
            {
                var sender = (ContentControl)depObject;
                sender.Content = Localization.GetByLocalizationKey(key);
            }
            else if (depObject is ContentPresenter)
            {
                var sender = (ContentPresenter)depObject;
                sender.Content = Localization.GetByLocalizationKey(key);
            }
        }

        #endregion

    }
}
