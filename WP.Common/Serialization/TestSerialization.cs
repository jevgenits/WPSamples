using System;
using System.IO;
using System.Diagnostics;
using WP.Common.Models;
using WP.Common.Serialization.XMLSerialization;

namespace WP.Common.Serialization
{
    public class TestSerialization
    {
        readonly Stopwatch _sw = new Stopwatch();

        public void Test()
        {
            // creating and filling sample data class
            //SampleData sd = new SampleData();
            //sd.Fill();

            //TestXMLSerializer(sd);
            //TestDataContractSerialization(sd);
            //TestDataContractJSONSerialization(sd);
            //TestDataContractBinarySerialization(sd);
            //TestJSONNETSerialization(sd); 
            //TestSilverlightSerializer(sd);
            //TestSharpSerializer(sd);
        }

        private double TestMethod(Action method)
        {
            _sw.Reset();
            _sw.Start();
            method.Invoke();
            _sw.Stop();
            return _sw.ElapsedMilliseconds;
        }

        public void TestXMLSerializer(BookModel sd)
        {
            using (var ms = new MemoryStream())
            {
                // time in milliseconds
                double serTime = TestMethod(() =>
                    XMLSerializerHelper.Serialize(ms, sd));
                long size = ms.Length;
                ms.Position = 0;
                double deSerTime = TestMethod(() =>
                    XMLSerializerHelper.Deserialize(ms, typeof(BookModel)));
            }
        }

        //public void TestDataContractSerialization(BookModel bm)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        // time in milliseconds
        //        double serTime = TestMethod(() =>
        //            DataContractSerializationHelper.Serialize(ms, bm));
        //        long size = ms.Length;
        //        ms.Position = 0;
        //        double deSerTime = TestMethod(() =>
        //            DataContractSerializationHelper.Deserialize(ms, typeof(SampleData)));
        //    }
        //}

        //public void TestDataContractJSONSerialization(SampleData sd)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        // time in milliseconds
        //        double serTime = TestMethod(() =>
        //            DataContractJSONSerializationHelper.Serialize(ms, sd));
        //        long size = ms.Length;
        //        ms.Position = 0;
        //        double deSerTime = TestMethod(() =>
        //            DataContractJSONSerializationHelper.Deserialize(ms, typeof(SampleData)));
        //    }
        //}

        //public void TestBinarySerialization(SampleData sd)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        // time in milliseconds
        //        double serTime = TestMethod(() =>
        //            BinarySerializationHelper.Serialize(ms, sd));
        //        long size = ms.Length;
        //        ms.Position = 0;
        //        double deSerTime = TestMethod(() =>
        //            BinarySerializationHelper.Deserialize(ms, typeof(SampleData)));
        //    }
        //}

        //public void TestJSONNETSerialization(SampleData sd)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        // time in milliseconds
        //        double serTime = TestMethod(() =>
        //            JSONNETSerializationHelper.Serialize(ms, sd));

        //        long size = ms.Length;
        //        ms.Position = 0;

        //        double deSerTime = TestMethod(() =>
        //        {
        //            JSONNETSerializationHelper.Deserialize(ms, typeof(SampleData));
        //        });
        //    }
        //}


        //public void TestSilverlightSerializer(SampleData sd)
        //{
        //    // NOT TESTED
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        // time in milliseconds
        //        double serTime = TestMethod(() =>
        //            SilverlightSerializerHelper.Serialize(ms, sd));

        //        long size = ms.Length;
        //        ms.Position = 0;

        //        double deSerTime = TestMethod(() =>
        //        {
        //            SilverlightSerializerHelper.Deserialize(ms, typeof(SampleData));
        //        });
        //    }
        //}

        //public void TestSharpSerializer(SampleData sd)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        // time in milliseconds
        //        double serTime = TestMethod(() =>
        //            SharpSerializerHelper.Serialize(ms, sd));

        //        long size = ms.Length;
        //        ms.Position = 0;

        //        double deSerTime = TestMethod(() =>
        //        {
        //            SharpSerializerHelper.Deserialize(ms);
        //        });
        //    }
        //}




    }
}
