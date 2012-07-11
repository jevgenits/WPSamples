using System.Xml.Serialization;

namespace WP.Common.Models
{
    [XmlRoot("books")]
    public class BookCollectionModel
    {
        [XmlElement("book")]
        public BookModel[] Books { get; set; }

    }
}
