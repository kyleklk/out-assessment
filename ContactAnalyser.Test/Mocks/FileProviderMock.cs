
using ContactAnalyser.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ContactAnalyser.Test.Mocks
{
    public delegate void StreamCloseDelegate(string str);
    public class FileProviderMock : IFileProvider
    {
        public string StreamContents { get; set; }

        public TextReader GetTextReader(string path)
        {
            var test = @"FirstName,LastName,Address,PhoneNumber
Test,Smith,1 B Lane,123456
Smith,Test,2 C Lane,123456
John,Doe,3 X Lane,123456";
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(test);
            writer.Flush();
            stream.Position = 0;
            return new StreamReader(stream);
        }

        public TextWriter GetTextWriter(string path)
        {
            var memStream = new CustomMemoryStream();
            memStream.StreamClosing += MemStream_StreamClosing;
            return new StreamWriter(memStream);
        }

        //this event handler will enable us to get the streams contents even if its used in a using statement in a consumer
        private void MemStream_StreamClosing(string str)
        {
            StreamContents = str;
        }
    }

    /// <summary>
    /// this class enables us to get the contents of the memory stream when it is closed
    /// by firing an event with the memory streams contents.
    /// </summary>
    public class CustomMemoryStream : MemoryStream
    {
        public event StreamCloseDelegate StreamClosing;
        public override void Close()
        {
            Position = 0;
            var sr = new StreamReader(this);
            var myStr = sr.ReadToEnd();
            StreamClosing(myStr);
            base.Close();
        }
    }
}
