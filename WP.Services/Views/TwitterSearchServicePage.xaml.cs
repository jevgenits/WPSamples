using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Phone.Controls;

namespace WP.Services.Views
{
    public partial class TwitterSearchServicePage : PhoneApplicationPage
    {
        const string TwitterSearchUrl = "http://search.twitter.com/search.atom?rpp=25&q={0}";

        public TwitterSearchServicePage()
        {
            InitializeComponent();
        }

        private void TweetSearchClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbTweetSearch.Text.Trim()))
            {
                string searchText = HttpUtility.UrlEncode(tbTweetSearch.Text.Trim());
                SearchTwitterEntries(new Uri(string.Format(TwitterSearchUrl, searchText), UriKind.Absolute));
            }
        }

        public void SearchTwitterEntries(Uri twitterSearchUri)
        {
            WebClient wclient = new WebClient();
            wclient.OpenReadCompleted += (sender, e) =>
            {
                if (e.Error != null) return;
                Stream str = e.Result; XElement xelem = XElement.Load(str);
                XNamespace atomNS = "http://www.w3.org/2005/Atom";
                List<TwitterEntry> twitterEntries = (from entry in xelem.Descendants(atomNS + "entry")
                                                     select new TwitterEntry()
                                                                {
                                                                    ID = entry.Element(atomNS + "id").Value,
                                                                    Title = entry.Element(atomNS + "title").Value,
                                                                    Date = DateTime.Parse(entry.Element(atomNS + "published").Value),
                                                                    AuthorName = entry.Descendants(atomNS + "author").Elements(atomNS + "name").FirstOrDefault().Value,
                                                                    AuthorUri = entry.Descendants(atomNS + "author").Elements(atomNS + "uri").FirstOrDefault().Value,
                                                                    AuthorImage = (from imgElement in entry.Elements(atomNS + "link") where imgElement.Attribute("rel") != null && imgElement.Attribute("rel").Value.Contains("image") && imgElement.Attribute("href") != null select imgElement.Attribute("href").Value).FirstOrDefault()
                                                                }).ToList();
                // close       
                str.Close();
                // add results to listbox        
                lbTwitterEntries.Items.Clear();
                twitterEntries.ForEach(item => lbTwitterEntries.Items.Add(item));
            };
            wclient.OpenReadAsync(twitterSearchUri);
        }
    }

    public class TwitterEntry
    {
        public string ID { get; set; }
        public string Title { get; set; } 
        public DateTime Date { get; set; }
        public string AuthorName { get; set; }
        public string AuthorUri { get; set; } 
        public string AuthorImage { get; set; }
    }
}