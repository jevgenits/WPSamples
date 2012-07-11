using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Resources;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using WP.Common.Models;

namespace WP.Common.XmlReaderSample
{
    public static class XmlParser
    {

        #region XmlReader samples

        public static List<BookModel> ParseBooksUsingXmlReader(Stream sr)
        {
            var books = new List<BookModel>();

            XmlReader xmlr = XmlReader.Create(sr);

            BookModel book = null;
            while (xmlr.Read())
            {
                if (xmlr.NodeType == XmlNodeType.Element)
                {
                    if (xmlr.Name == "book")
                    {
                        book = new BookModel();
                        // parsing id attribute
                        book.Id = long.Parse(xmlr["id"]);
                    }
                    else if (xmlr.Name == "title")
                    {
                        if (book != null)
                            book.Title = xmlr.ReadElementContentAsString();
                    }
                    else if (xmlr.Name == "description")
                    {
                        if (book != null)
                            book.Description = xmlr.ReadElementContentAsString();
                    }
                    else if (xmlr.Name == "year")
                    {
                        if (book != null)
                            book.Year = xmlr.ReadElementContentAsInt();

                        // add book to collection
                        books.Add(book);
                    }

                }
            }

            return books;
        }

        #endregion XmlReader samples

        #region LinqToXml samples

        public static List<BookModel> ParseBooksUsingLinqToXml(Stream sr)
        {
            XDocument xdoc = XDocument.Load(sr);
            return (from book in xdoc.Descendants("book")
                    let idAttribute = book.Attribute("id")
                    where idAttribute != null
                    select new BookModel()
                               {
                                   Id = long.Parse(idAttribute.Value),
                                   Title = GetElementValueOrDefault(book.Element("title")),
                                   Description = GetElementValueOrDefault(book.Element("description")),
                                   Year = int.Parse(GetElementValueOrDefault(book.Element("year")))
                               }).ToList();

        }

        #endregion LinqToXml samples

        #region Parsing using Xml Deserialization

        public static List<BookModel> ParseBooksUsingXmlDeserialization(Stream sr)
        {
            var serializer = new XmlSerializer(typeof(BookCollectionModel));
            return ((BookCollectionModel) serializer.Deserialize(sr)).Books.ToList();
        }  

        #endregion Parsing using Xml Deserialization

        #region Utils

        private static string GetElementValueOrDefault(XElement element)
        {
            return (element != null) ? element.Value : null;
        }

        private static string GetAttributeValueOrDefault(XAttribute attribute)
        {
            return (attribute != null) ? attribute.Value : null;
        }

        #endregion Utils

        #region Compare

        public static void CompareParsers()
        {
            var sw = new Stopwatch();


            StreamResourceInfo xmlStream1 = Application.GetResourceStream(new Uri("/WP.Common;component/Data/books3.xml", UriKind.Relative));
            sw.Start();
            var obj3 = XmlParser.ParseBooksUsingXmlReader(xmlStream1.Stream);
            sw.Stop();
            Debug.WriteLine(string.Format("Parsing using XmlReader: {0} ms", sw.ElapsedMilliseconds));
            xmlStream1.Stream.Close();
            sw.Reset();
            Thread.Sleep(1000);

            StreamResourceInfo xmlStream2 = Application.GetResourceStream(new Uri("/WP.Common;component/Data/books2.xml", UriKind.Relative));
            sw.Start();
            var obj1 = XmlParser.ParseBooksUsingLinqToXml(xmlStream2.Stream);
            sw.Stop();
            Debug.WriteLine(string.Format("Parsing using LinqToXml: {0} ms", sw.ElapsedMilliseconds));
            xmlStream2.Stream.Close();
            sw.Reset();
            Thread.Sleep(1000);

            StreamResourceInfo xmlStream3 = Application.GetResourceStream(new Uri("/WP.Common;component/Data/books.xml", UriKind.Relative));
            sw.Start();
            DateTime dt2 = DateTime.Now;
            var obj2 = XmlParser.ParseBooksUsingXmlDeserialization(xmlStream3.Stream);
            sw.Stop();
            Debug.WriteLine(string.Format("Parsing using Xml Deserialization: {0} ms", sw.ElapsedMilliseconds));
            xmlStream3.Stream.Close();
            sw.Reset();
            Thread.Sleep(1000);
        }

        #endregion Compare

    }


}
