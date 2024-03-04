using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.IO.Files
{
    public static class MrFileExentions
    {

        /// <summary>
        /// compress file from file`s stream and save it in spacific path
        /// after Encrypt it
        /// </summary>
        /// <param name="data"></param>
        /// <returns  compressed stream ></returns>
        internal async static Task<Stream> Compress(this Stream fileStream, string path, string encyptionKey)
        {
            if (fileStream is null || String.IsNullOrEmpty(path))
                throw new InvalidDataException();

            using (FileStream compressedFileStream = File.Create(path))
            {
                using (DeflateStream compressionStream = new DeflateStream(compressedFileStream,
                   CompressionMode.Compress))
                {

                    var compression =  !String.IsNullOrEmpty(encyptionKey) ?
                                       compressionStream.FileEncrypt(encyptionKey) : compressionStream;

                    await fileStream.CopyToAsync(compression);

                }
            }
            return fileStream;
        }
        /// <summary>
        /// compress file from file`s stream and save it in spacific path
        /// </summary>
        /// <param name="data"></param>
        /// <returns  compressed stream ></returns>
        public async static Task<Stream> Compress(this Stream fileStream, string path)
        {
            return await Compress(fileStream, path, null);
        }



        /// <summary>
        /// decompress file from file`s  stream  which was compressed by GZipStream
        /// </summary>
        /// <param name="data"></param>
        /// <returns decompressed bytes ></returns>
        public async static Task<byte[]> Decompress(this Stream fileStream)
        {
            if (fileStream is null) throw new InvalidDataException();

            MemoryStream output = new MemoryStream();

            using (GZipStream decompressionStream = new GZipStream(fileStream, CompressionMode.Decompress))
            {
                await decompressionStream.CopyToAsync(output);
            }

            return output.ToArray();
        }

        /// <summary>
        ///  compress file from file`s bytes or any string from it`s bytes array
        /// </summary>
        /// <param name="data"></param>
        /// <returns ></returns>
        public static byte[] Compress(this byte[] data)
        {
            if (!data.Any()) throw new InvalidDataException();

            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
            {
                dstream.Write(data, 0, data.Length);
            }

            return output.ToArray();
        }

        /// <summary>
        /// decompress file from file`s  bytes or any string from it`s bytes array
        /// which was compressed by DeflateStream
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async static Task<byte[]> Decompress(this byte[] data)
        {
            if (!data.Any()) throw new InvalidDataException();

            MemoryStream output = new MemoryStream();

            using (DeflateStream dstream = new DeflateStream(new MemoryStream(data), CompressionMode.Decompress))
            {
                await dstream.CopyToAsync(output);
            }

            return output.ToArray();
        }
        /// <summary>
        /// get file extention from file bytes 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static FileExtensions GetFileFormat(this byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };    // PNG
            var png1 = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 };
            var tiff = new byte[] { 73, 73, 42 };         // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };         // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 };  // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon
            var JPEG = new byte[] { 255, 216, 255, 237 };  // JPEG 
            var pdf = new byte[] { 37, 80, 68, 70, 45, 49, 46 };   // PDF 
            var doc = new byte[] { 208, 207, 17, 224, 161, 177, 26, 225 };
            var mp3 = new byte[] { 255, 251, 48 };
            var rar = new byte[] { 82, 97, 114, 33, 26, 7, 0 };
            var swf = new byte[] { 70, 87, 83 };


            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return FileExtensions.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return FileExtensions.gif;

            if (png1.SequenceEqual(bytes.Take(png1.Length)))
                return FileExtensions.png;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return FileExtensions.png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return FileExtensions.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return FileExtensions.tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return FileExtensions.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return FileExtensions.jpeg;

            if (JPEG.SequenceEqual(bytes.Take(JPEG.Length)))
                return FileExtensions.JPEG;

            if (pdf.SequenceEqual(bytes.Take(pdf.Length)))
                return FileExtensions.pdf;

            if (doc.SequenceEqual(bytes.Take(doc.Length)))
                return FileExtensions.doc;

            if (mp3.SequenceEqual(bytes.Take(mp3.Length)))
                return FileExtensions.mp3;

            if (rar.SequenceEqual(bytes.Take(rar.Length)))
                return FileExtensions.rar;

            if (swf.SequenceEqual(bytes.Take(swf.Length)))
                return FileExtensions.swf;

            return FileExtensions.unknown;
        }
        /// <summary>
        ///  Cleaning base64 of extra characters
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        internal static  string FormateBase64(this string base64String)
        {
            if (String.IsNullOrEmpty(base64String)) throw new InvalidDataException();

            int unOrganizedChar = base64String.IndexOf(',') + 1;

            var result = base64String.Substring(unOrganizedChar ,
                base64String.Length - unOrganizedChar);

            return result;
        }

        /// <summary>
        ///  get file name from file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        internal static string GetFileNameFromPath(this string filePath)
        {
             return (!String.IsNullOrEmpty(filePath)) ? Path.GetFileName(filePath) : throw new InvalidDataException();
        }

        /// <summary>
        ///  generate random bytes which can convert to base64 to get strong random string
        /// </summary>
        /// <returns></returns>
        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    // Fille the buffer with the generated data
                    rng.GetBytes(data);
                }
            }

            return data;
        }

        /// <summary>
        /// File Encryption 
        /// </summary>
        /// <param name="fileStream" is stream of file need ecryption ></param>
        /// <param name="enKey" Specific key to encrypt the file ></param>
        /// <returns></returns>
        public static Stream FileEncrypt(this Stream fileStream, string enKey)
        {
            //generate random salt
            byte[] salt = GenerateRandomSalt();

            //convert password string to byte arrray
            byte[] passwordBytes = Encoding.UTF8.GetBytes(enKey);

            //Set Rijndael symmetric encryption algorithm
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.None;

            //"What it does is repeatedly hash the user password along with the salt." High iteration counts.
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            AES.Mode = CipherMode.CFB;

            // write salt to the begining of the output file, so in this case can be random every time
            fileStream.Write(salt, 0, salt.Length);

            CryptoStream cs = new CryptoStream(fileStream, AES.CreateEncryptor(), CryptoStreamMode.Write);
            return cs;

        }

        /// <summary>
        ///  Extention method of stream to convert it to memory stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static MemoryStream ConvertStreamToMemoryStream(this Stream stream)
        {
            MemoryStream memoryStream = new MemoryStream();

            if (stream != null)
            {
                byte[] buffer = stream.ReadFully();

                if (buffer != null)
                {
                    var binaryWriter = new BinaryWriter(memoryStream);
                    binaryWriter.Write(buffer);
                }
            }
            return memoryStream;
        }

        public static byte[] ReadFully(this Stream stream)
        {

            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }


        /// <summary>
        /// File Decryption 
        /// </summary>
        /// <param name="fileStream" is stream of file need decryption ></param>
        /// <param name="enKey"  spacific key for decrypt the file which should be identical with encryption key></param>
        /// <returns></returns>
        internal static Stream FileDecrypt(this Stream fileStream, string decKey)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(decKey);

            byte[] salt = new byte[32];

            fileStream.Read(salt, 0, salt.Length);

            RijndaelManaged AES = new RijndaelManaged();

            AES.KeySize = 256;
            AES.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);

            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.None;
            AES.Mode = CipherMode.CFB;

            CryptoStream cs = new CryptoStream(fileStream, AES.CreateDecryptor(), CryptoStreamMode.Read);
            return cs;
        }

        /// <summary>
        /// String Encryption
        /// </summary>
        /// <param name="file"></param>
        /// <param name="keyString"></param>
        /// <returns></returns>
        public static byte[] EncryptString(this byte[] text, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return result;
                    }
                }
            }
        }

        internal static string GetPath(this Enum Catagory, MrFileOption fileOption)
        {
            if ((Catagory, fileOption).CheckFileCatagory())
            {
                throw new InvalidDataException("File cetagory is incorrect");
            }

            string path = Path.Combine(fileOption.FinalFilePath, Catagory.ToString());

            return path;
        }


        private static bool CheckFileCatagory(this (Enum Catagory, MrFileOption option) chackFile) =>
            !(chackFile.Catagory.GetType().IsEnum 
            && (chackFile.option.Categories is null || chackFile.option.Categories.Contains(chackFile.Catagory.ToString())));
       
    }





}
