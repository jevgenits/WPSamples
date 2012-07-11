using System.Xml.Serialization;

namespace WP.Common.Models
{
    public class BookModel
    {
        [XmlAttribute(AttributeName = "id")]
        public long Id { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "year")]
        public int Year { get; set; }
    }

}
