using System;
using System.IO;
using System.Xml.Serialization;

namespace WP.Common.Serialization.XMLSerialization
{
    public class XMLSerializationHelper
    {
        public static void Serialize(Stream streamObject, object objForSerialization)
        {
            if (objForSerialization == null || streamObject == null)
                return;

            var serializer = new XmlSerializer(objForSerialization.GetType());
            serializer.Serialize(streamObject, objForSerialization);
        }

        public static object Deserialize(Stream streamObject, Type serializedObjectType)
        {
            if (serializedObjectType == null || streamObject == null)
                return null;

            var serializer = new XmlSerializer(serializedObjectType);
            return serializer.Deserialize(streamObject);
        }
    }
}
