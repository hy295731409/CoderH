using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.Utility.Helper
{
    public class ByteHelper
    {
        public static Stream ByteToStream(byte[] buffer)
        {
            var stream = new MemoryStream(buffer);
            return stream;
        }
        public static byte[] StreamTobytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
    }
}
