using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml.Linq;
using WP.Common.Models;

namespace WP.Common.LinqToXmlSample
{
    public static class LinqToXmlParser
    {
        public static IEnumerable<BookModel> ParseBooks()
        {
            var xml =
                Application.GetResourceStream(new Uri("/EugeneDotnetCommon;component/Data/books.xml", UriKind.Relative));
            using (var sr = new StreamReader(xml.Stream, Encoding.UTF8))
            {
                // Load XDocument
                //var xdoc = XDocument.Parse(sr.ReadToEnd());

                // Load Root Element
                var rootElement = XElement.Load(sr);
                foreach (XElement book in rootElement.Descendants("book"))
                    yield return new BookModel()
                    {
                        Id = long.Parse(GetAttributeValueOrDefault(book.Attribute("id"))),
                        Title = GetElementValueOrDefault(book.Element("title")),
                        Description = GetElementValueOrDefault(book.Element("description")),
                        Year = int.Parse(GetElementValueOrDefault(book.Element("year")))
                    };
            }
        }

        private static string GetElementValueOrDefault(XElement element)
        {
            return (element != null) ? element.Value : null;
        }

        private static string GetAttributeValueOrDefault(XAttribute attribute)
        {
            return (attribute != null) ? attribute.Value : null;
        }
    }
}
