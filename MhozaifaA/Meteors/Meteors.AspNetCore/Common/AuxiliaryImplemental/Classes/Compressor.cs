using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.AuxiliaryImplemental.Classes
{
    public class Compressor
    {
        public static void CompressTo(Stream stream, Stream outputStream)
        {
            using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
            {
                stream.CopyTo(gZipStream);
                gZipStream.Flush();
            }
        }
        public static byte[] Compress( byte[] data)
        {
            using (var sourceStream = new MemoryStream(data))
            using (var destinationStream = new MemoryStream())
            {
                CompressTo(sourceStream,destinationStream);
                return destinationStream.ToArray();
            }
        }
        public static string CompressToBase64( string data)
        {
            return Convert.ToBase64String(Compress(Encoding.UTF8.GetBytes(data)));
        }

        //public static string CompressToBase64( object data)
        //{
        //    var settings = new JsonSerializerOptions
        //    {
        //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //    };
        //    return CompressToBase64(JsonSerializer.Serialize(data, settings));
        //}


        public static string DecompressFromBase64( string data)
        {
            return Encoding.UTF8.GetString(Decompress(Convert.FromBase64String(data)));
        }

        public static byte[] Decompress(byte[] data)
        {
            using (var sourceStream = new MemoryStream(data))
            using (var destinationStream = new MemoryStream())
            {
                DecompressTo(sourceStream,destinationStream);
                return destinationStream.ToArray();
            }
        }

        public static void DecompressTo(Stream stream, Stream outputStream)
        {
            using (var gZipStream = new GZipStream(stream, CompressionMode.Decompress))
            {
                gZipStream.CopyTo(outputStream);
            }
        }
    }
}
