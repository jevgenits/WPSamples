using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using WP.Services.MicrosoftTagService;

namespace WP.Services.Views
{
    public partial class MicrosoftTagServicePage : PhoneApplicationPage
    {
        
        #region Properties

        public string TagAccessToken
        {
            get
            {
                throw new Exception("You need to provide your own token");
                // 1. Sign up for MS Tag http://tag.microsoft.com/home.aspx
                // 2. Request Token from http://tag.microsoft.com/ws/accessrequest.aspx
                return "<enter your token here>";
            }
        }

        public string TagCategoryName
        {
            get
            {
                return textboxCategory.Text.Trim();
            }
        }

        public string TagURI
        {
            get
            {
                return textBoxURI.Text.Trim();
            }
        }

        public string TagName
        {
            get
            {
                return textBoxTagName.Text.Trim();
            }
        }

        public UserCredential TagCredential
        {
            get
            {
                return new UserCredential() { AccessToken = TagAccessToken };
            }
        }

        private MIBPContractClient client;
        public MIBPContractClient TagClient
        {
            get
            {
                if (client == null)
                {
                    client = new MIBPContractClient();
                    client.CreateCategoryCompleted += (sender, e) => { CreateMSTag();  };
                    client.CreateTagCompleted += (sender, e) => { GetMSTagBarcode(); };
                    client.GetBarcodeCompleted += (sender, e) => { RenderBarcode(e.Result); };
                }
                return client;
            }
        }

        #endregion Properties

        #region Constructor

        public MicrosoftTagServicePage()
        {
            InitializeComponent();

            btnGenerate.IsEnabled = true;
            imageTag.DataContext = null;
            cbIsNew.IsChecked = false;
        }

        #endregion Constructor

        #region Events

        private void GenerateClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TagAccessToken)
                && !string.IsNullOrEmpty(TagCategoryName)
                && !string.IsNullOrEmpty(TagURI)
                )
            {
                btnGenerate.IsEnabled = false;
                imageTag.DataContext = null;
                this.Focus();
                if (cbIsNew.IsChecked != null && !cbIsNew.IsChecked.Value)
                {
                    CreateMSTag();
                }
                else
                {
                    CreateCategory();
                }
                
            }
        }

        #endregion Events

        #region Create Microsoft Tag

        private void CreateMSTag()
        {
            try
            {
                TagClient.CreateTagAsync(TagCredential, TagCategoryName, CreateURITag(TagURI));
            }
            catch
            {
                throw;
            }
        }

        #endregion Create Microsoft Tag

        #region Microsoft Tag Category

        private void CreateCategory()
        {
            try
            {
                // no need to check existence
                Category oCat = new Category();
                oCat.Name = TagCategoryName;
                oCat.UTCStartDate = DateTime.Now;
                oCat.UTCEndDate = new DateTime(2012, 01, 01);
                TagClient.CreateCategoryAsync(TagCredential, oCat);
            }
            catch
            {
                throw;
            }
        }

        #endregion Microsoft Tag Category

        #region Microsoft Tag Barcode

        private void GetMSTagBarcode()
        {
            try
            {
                TagClient.GetBarcodeAsync(TagCredential, TagCategoryName, TagName,
                    ImageTypes.png, 3, DecorationType.HCCBRP_DECORATION_NONE, false);
            }
            catch
            {
                throw;
            }

        }

        private void RenderBarcode(byte[] result)
        {
            if (result != null)
            {
                Stream _s = new MemoryStream(result);
                _s.Position = 0;
                BitmapImage _img = new BitmapImage();
                _img.SetSource(_s);
                imageTag.Source = _img;
            }
            btnGenerate.IsEnabled = true;
        }

        #endregion Microsoft Tag Barcode

        #region URI Tag

        private URITag CreateURITag(string uri)
        {
            URITag uriTag = new URITag();
            uriTag.MedFiUrl = uri;
            uriTag.Title = TagName;
            uriTag.UTCStartDate = DateTime.Now;
            uriTag.UTCEndDate = new DateTime(2012, 01, 01);
            return uriTag;
        }

        #endregion URI Tag

    }
}