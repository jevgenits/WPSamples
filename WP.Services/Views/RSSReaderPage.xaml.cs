using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Phone.Controls;

namespace WP.Services.Views
{
    public partial class RSSReaderPage : PhoneApplicationPage
    {
        public RSSReaderPage()
        {
            InitializeComponent();
        }

        private void ReadRSSClick(object sender, RoutedEventArgs e)
        {
            ReadRss(new Uri("http://www.codeproject.com/WebServices/ArticleRSS.aspx?cat=1", UriKind.Absolute));
        }

        public void ReadRss(Uri rssUri)
        {
            WebClient wclient = new WebClient();
            wclient.OpenReadCompleted += (sender, e) =>
                                             {
                                                 if (e.Error != null)
                                                     return;
                                                 Stream str = e.Result;
                                                 XDocument xdoc = XDocument.Load(str);
                                                 // take 100 first results      
                                                 var rssFeedItems = (from item in xdoc.Descendants("item")
                                                                     select new RSSFeedItem()
                                                                                              {
                                                                                                  Title = item.Element("title").Value,
                                                                                                  Description = item.Element("description").Value
                                                                                              }).Take(100).ToList();
                                                 // close    
                                                 str.Close();
                                                 // add results to listbox   
                                                 listboxRSSFeedItems.Items.Clear();
                                                 rssFeedItems.ForEach(item => listboxRSSFeedItems.Items.Add(item));
                                             };
            wclient.OpenReadAsync(rssUri);
        }
    }

    public class RSSFeedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}