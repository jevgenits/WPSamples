using System.ComponentModel;
using Microsoft.Phone.Controls;

namespace WP.Basics.Navigation
{
    public partial class NavigationPage2 : PhoneApplicationPage, INotifyPropertyChanged
    {

        private string _textToDisplay;

        public string TextToDisplay
        {
            get { return _textToDisplay; }
            set
            {
                _textToDisplay = value;
                NotifyPropertyChanged("TextToDisplay");
            }
        }

        public NavigationPage2()
        {
            InitializeComponent();
            DataContext = this;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if(NavigationContext.QueryString.ContainsKey("p"))
                TextToDisplay = NavigationContext.QueryString["p"];
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

    }
}