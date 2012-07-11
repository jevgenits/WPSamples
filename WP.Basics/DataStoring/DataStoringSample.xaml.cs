using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WP.Basics.DataStoring
{
    public partial class DataStoringToServiceSample : PhoneApplicationPage, INotifyPropertyChanged
    {
        private const string IsolatedStorageFileName = "somefile.txt";
        private const string Key = "someKey";

        public DataStoringToServiceSample()
        {
            InitializeComponent();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        private void SaveToPageState(object sender, RoutedEventArgs e)
        {
            // object must be serializable
            // 2Mb of data per page
            // 4Mb of data per application
            // available between OnNavigatedTo and OnNavigatedFrom
            this.State[Key] = tbTextBox.Text.Trim();
            tbTextBox.Text = string.Empty;
        }
        
        private void GetFromPageState(object sender, RoutedEventArgs e)
        {
            if (this.State.ContainsKey(Key))
                tbText.Text = this.State[Key] as string;
        }

        private void SaveToApplicationService(object sender, RoutedEventArgs e)
        {
            // object must be serializable
            // available between 
            PhoneApplicationService.Current.State[Key] = tbTextBox.Text.Trim();
            tbTextBox.Text = string.Empty;
        }

        private void GetFromApplicationService(object sender, RoutedEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey(Key))
                tbText.Text = PhoneApplicationService.Current.State[Key] as string;
        }

        private void SaveToIsolatedStorage(object sender, RoutedEventArgs e)
        {
            using (var isoFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isoFile.FileExists(IsolatedStorageFileName))
                    isoFile.DeleteFile(IsolatedStorageFileName);

                using(var isoFileStream = new StreamWriter(isoFile.CreateFile(IsolatedStorageFileName)))
                {
                    isoFileStream.WriteLine(tbTextBox.Text.Trim());
                }
            }
            tbTextBox.Text = string.Empty;
        }
        
        private void GetFromIsolatedStorage(object sender, RoutedEventArgs e)
        {
            using (var isoFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isoFile.FileExists(IsolatedStorageFileName))
                {
                    using (var isoFileStream = new StreamReader(
                        isoFile.OpenFile(IsolatedStorageFileName, FileMode.Open)))
                    {
                        tbText.Text = isoFileStream.ReadLine();
                    }
                }
            }
        }

        private void SaveToIsolatedStorageSettings(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings.ApplicationSettings[Key] = tbTextBox.Text.Trim();
            IsolatedStorageSettings.ApplicationSettings.Save();
            tbTextBox.Text = string.Empty;
        }

        private void GetFromIsolatedStorageSettings(object sender, RoutedEventArgs e)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(Key))
                tbText.Text = IsolatedStorageSettings.ApplicationSettings[Key] as string;   
        }
    }
}