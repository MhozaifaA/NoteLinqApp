using Meteors.AspNetCore.Helper.ExtensionMethods.Enumerable;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Meteors.AspNetCore.IO.Files
{
    public class MrFileService : IMrFileService
    {
        public readonly MrFileOption _FileOption;

        public MrFileService(IOptions<MrFileOption> option=null)
        {
            _FileOption= option?.Value??new MrFileOption();
        }

        /// <summary>
        ///  upload multiple Ifomefiles in spacific path with compression mode
        /// </summary>
        /// <param name="filePath" >a special path where files are saved</param>
        /// <param name="files" > List of file information to be saved</param>
        /// <param name="MrCreationFileMode" >save file with compress or not </param>
        /// <param name="encyptionKey" >encyption key for encyption mode </param>
        /// <returns>list of uploaded files paths</returns>
        public async Task<IEnumerable<string>> UploadMultiFilesAsync(string filePath, IFormFileCollection files, MrCreationFileMode MrCreationFileMode, string encyptionKey)
        {
            var result = new List<string>();

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            foreach (var item in files)
            {
                if (item != null)
                {
                    filePath = Path.Combine(filePath, $"{Guid.NewGuid()}-{MrCreationFileMode}-{(String.IsNullOrEmpty(encyptionKey)? MrEncryptionMode.Decryption: MrEncryptionMode.Encryption)}-{item.FileName}");
                    if (MrCreationFileMode == MrCreationFileMode.Compress)
                    {
                        using (var stream = item.OpenReadStream())
                        {
                            await stream.Compress(filePath, encyptionKey);
                        }
                    }
                    else
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await item.CopyToAsync((String.IsNullOrEmpty(encyptionKey) ?
                                      fileStream : fileStream.FileEncrypt(encyptionKey)));
                        }
                    }
                    result.Add(filePath);

                }
            }

            return result;
        }

     
        /// <summary>
        ///  uploading multiple Iformfile in specific path 
        /// </summary>
        /// <param name="filePath">spacific path to save files </param>
        /// <param name="files"> list of files information as IformFile  </param>
        /// <param name="MrCreationFileMode">create file with compression mode or not</param>
        /// <returns list of uploaded files paths></returns>
        public async Task<IEnumerable<string>> UploadMultiFilesAsync(string filePath, IFormFileCollection files, MrCreationFileMode MrCreationFileMode)
        {
            return await UploadMultiFilesAsync(filePath, files, MrCreationFileMode, String.Empty);

        }

        /// <summary>
        ///  uploading multiple Iformfile in specific path with compression mode
        /// </summary>
        /// <param name="filePath">spacific path to save files </param>
        /// <param name="files"> list of files information as IformFile  </param>
        /// <param name="MrCreationFileMode">create file with compression mode or not</param>
        /// <param name="encyptionKey">encyption key for encyption mode</param>
        /// <returns>list of uploaded files paths</returns>
        public async Task<IEnumerable<string>> UploadMultiFilesAsync(string filePath, Base64FileCollection files, MrCreationFileMode MrCreationFileMode, string encyptionKey)
        {
            var result = new List<string>();

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            byte[] fileBytes = Array.Empty<byte>();


            foreach (var item in files)
            {
                if (item != null)
                {
                    var formattedString = item.File.FormateBase64();
                    fileBytes = Convert.FromBase64String(formattedString.FormateBase64());

                    filePath = Path.Combine(filePath, $"{item.FileName}-{((!String.IsNullOrEmpty(encyptionKey)) ? MrEncryptionMode.Encryption : String.Empty)}-{MrCreationFileMode}-{Guid.NewGuid()}.{fileBytes.GetFileFormat()}");

                    using (var file = new FileStream(filePath, FileMode.Create))
                    {

                        fileBytes = (MrCreationFileMode == MrCreationFileMode.Compress) ?
                        fileBytes.Compress() : fileBytes;
                        if (!String.IsNullOrEmpty(encyptionKey))
                        {
                            var memory = new MemoryStream(fileBytes);
                            memory.CopyTo(file.FileEncrypt(encyptionKey));
                        }
                        else
                            await file.WriteAsync(fileBytes, 0, fileBytes.Length);

                        result.Add(filePath);
                    }

                }
            }

            return result;
        }
        public async Task<IEnumerable<string>> UploadMultiFilesAsync(MrCreationFileMode fileMode, Enum Catagory, Base64FileCollection files, string encyptionKey)
        {
            return (await UploadMultiFilesAsync(Catagory.GetPath(_FileOption), files, fileMode,encyptionKey));
        }

        /// <summary>
        ///  uploading Iformfile in specific path with compression mode
        /// </summary>
        /// <param name="filePath">spacific path to save files </param>
        /// <param name="file"> file information as IformFile  </param>
        /// <param name="MrCreationFileMode">create file with compression mode or not</param>
        /// <param name="encyptionKey">encyption key for encyption mode</param>
        /// <returns>uploaded files path</returns>
        public async Task<string> UploadFileAsync(string filePath, IFormFile file, MrCreationFileMode MrCreationFileMode, string encyptionKey)
        {
            return (await UploadMultiFilesAsync(filePath, new FormFileCollection() { file }, MrCreationFileMode, encyptionKey)).FirstOrDefault();
        }

        public async Task<string> UploadFileAsync(string filePath, Base64file file, MrCreationFileMode MrCreationMode, string encyptionKey)
        {
            return (await UploadMultiFilesAsync(filePath, new Base64FileCollection().AddBase64file(file), MrCreationMode, encyptionKey)).FirstOrDefault();
        }

        /// <summary>
        ///  Update list of files as Base64file in configred path with compression mode
        /// </summary>
        /// <param name="files"> file information as IformFile  </param>
        /// <param name="encyptionKey">encyption key for encyption mode</param>
        /// <returns>updated files paths list</returns>
        public async Task<IEnumerable<string>> UpdateMultiFilesAsync(IEnumerable<UpdateBase64File> files, string encyptionKey)
        {
            foreach (var file in files)
            {
                if (RemoveFileAsync(file.OldFileUrl))
                {
                    await UploadFileAsync(file.FileMode, file.Catagory, file.File, encyptionKey);
                }

            }
            return new List<string>();
        }

        /// <summary>
        ///  uploading list of files as Base64file in configred path with compression mode
        /// </summary>
        /// <param name="files"> file information as IformFile  </param>
        /// <param name="encyptionKey">encyption key for encyption mode</param>
        /// <returns>updated files paths list</returns>
        public async Task<IEnumerable<string>> UpdateMultiFilesAsync(IEnumerable<UpdateFormFile> files, string encyptionKey)
        {
            foreach (var file in files)
            {
                if (RemoveFileAsync(file.OldFileUrl))
                {
                    await UploadFileAsync(file.FileMode, file.Catagory, file.File, encyptionKey);
                }

            }
            return new List<string>();
        }

        /// <summary>
        ///  Update file (Base64file)
        ///  delete old file by sended path and upload new Base64file
        /// </summary>
        /// <param name="fileInfo"> update information</param>
        /// <param name="encyptionKey">encyption key for encyption mode</param>
        /// <returns >updated files paths list</returns>
        public async Task<string> UpdateFileAsync(UpdateBase64File fileInfo, string encyptionKey)
        {
            return (await UpdateMultiFilesAsync(new List<UpdateBase64File>() { fileInfo }, encyptionKey)).FirstOrDefault();
        }

        /// <summary>
        ///  Update file (Base64file)
        ///  delete old file by sended path and upload new Base64file
        /// </summary>
        /// <param name="fileInfo"> update information</param>
        /// <param name="encyptionKey">encyption key for encyption mode</param>
        /// <returns >updated file path </returns>
        public async Task<string> UpdateFileAsync(UpdateFormFile fileInfo, string encyptionKey)
        {
            return (await UpdateMultiFilesAsync(new List<UpdateFormFile>() { fileInfo }, encyptionKey)).FirstOrDefault();
        }

        public bool RemoveFileAsync(string filePath)
        {
            return RemoveMultiFilesAsync(new List<string>() { filePath });
        }

        public bool RemoveMultiFilesAsync(IEnumerable<string> filesPaths)
        {
            foreach (var file in filesPaths)
            {
                if (!file.IsNotNullOrEmpty())
                {
                    File.Delete(file);
                }
                else
                {
                    return false;
                }

            }
            return true;

        }

        /// <summary>
        /// Getting a fixed file by its path after decompressing it
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>fixed file bytes</returns>
        public async Task<byte[]> GetFile(string filePath)
        {
            if (String.IsNullOrEmpty(filePath)) throw new InvalidDataException();
            var bytes = await File.ReadAllBytesAsync(filePath);
            if (filePath.Contains(nameof(CompressionMode.Compress)))
                bytes = await bytes.Decompress();

            return bytes;
        }
        /// <summary>
        ///  Getting correct file by it`s path after decompressed and Decrypt it
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<byte[]> GetDecryptedFile(string dencyptionKey, string filePath)
        {
            if (String.IsNullOrEmpty(filePath)) throw new InvalidDataException();

            var bytes = await File.ReadAllBytesAsync(filePath);

            if (filePath.Contains(nameof(CompressionMode.Compress)))
                bytes = await bytes.Decompress();

            if (filePath.Contains(nameof(MrEncryptionMode.Encryption)))
            {
                MemoryStream stream = new MemoryStream(bytes);

                var decryptedStream = stream.FileDecrypt(dencyptionKey);

                MemoryStream memoryStream = new MemoryStream();

                await decryptedStream.CopyToAsync(memoryStream);

                bytes = memoryStream.ToArray();
            }
            return bytes;
        }

        public async Task<string> UploadFileAsync(string path, IFormFile file, MrCreationFileMode MrCreationMode)
        {
            return (await UploadMultiFilesAsync(path, new FormFileCollection() { file }, MrCreationMode, null)).FirstOrDefault();
        }

        public async Task<IEnumerable<string>> UploadMultiFilesAsync(string path, Base64FileCollection files, MrCreationFileMode MrCreationFileMode)
        {
            return await UploadMultiFilesAsync(path, files, MrCreationFileMode, null);
        }

        public async Task<string> UploadFileAsync(string path, Base64file file, MrCreationFileMode CreationFileMode)
        {
            return await UploadFileAsync(path, file, CreationFileMode);
        }

        public async Task<string> UpdateFileAsync(UpdateFormFile fileInfo)
        {
            return await UpdateFileAsync(fileInfo, null);
        }

        public async Task<IEnumerable<string>> UpdateMultiFilesAsync(IEnumerable<UpdateFormFile> filesInfo)
        {
            return await UpdateMultiFilesAsync(filesInfo, null);
        }

        public async Task<IEnumerable<string>> UploadMultiFilesAsync(MrCreationFileMode fileMode, Enum Catagory, IFormFileCollection files)
        {
            return await UploadMultiFilesAsync(fileMode, Catagory,files ,String.Empty);
        }

        public async Task<string> UploadFileAsync(MrCreationFileMode fileMode, Enum Catagory, IFormFile file, MrEncryptionMode MrEncryptionMode, string encyptionKey)
        {
            return (await UploadMultiFilesAsync(fileMode, Catagory,new FormFileCollection() { file }, encyptionKey)).FirstOrDefault();
        }

        public async Task<string> UploadFileAsync(MrCreationFileMode fileMode, Enum Catagory, IFormFile file)
        {
            return (await UploadMultiFilesAsync(fileMode, Catagory, new FormFileCollection() { file }, String.Empty)).FirstOrDefault();
        }

        public async Task<IEnumerable<string>> UploadMultiFilesAsync(MrCreationFileMode fileMode, Enum Catagory, IFormFileCollection files, string encyptionKey)
        {
            return (await UploadMultiFilesAsync(Catagory.GetPath(_FileOption), files, fileMode, encyptionKey));
        }

        public async Task<string> UploadFileAsync(MrCreationFileMode fileMode, Enum Catagory, IFormFile file, string encyptionKey)
        {
            return (await UploadMultiFilesAsync(fileMode, Catagory ,new FormFileCollection() { file }, encyptionKey)).FirstOrDefault();
        }

       

        public async Task<IEnumerable<string>> UploadMultiFilesAsync(MrCreationFileMode fileMode, Enum Catagory, Base64FileCollection files)
        {
            return await UploadMultiFilesAsync(fileMode, Catagory , files , null);
        }

        public async Task<string> UploadFileAsync(MrCreationFileMode fileMode, Enum Catagory, Base64file file, string encyptionKey)
        {
            return (await UploadMultiFilesAsync(fileMode, Catagory ,new Base64FileCollection().AddBase64file(file) , encyptionKey)).FirstOrDefault();
        }

        public async Task<string> UploadFileAsync(MrCreationFileMode fileMode, Enum Catagory, Base64file file)
        {
            return (await UploadMultiFilesAsync(fileMode, Catagory, new Base64FileCollection().AddBase64file(file), String.Empty)).FirstOrDefault();
        }

    }
}
