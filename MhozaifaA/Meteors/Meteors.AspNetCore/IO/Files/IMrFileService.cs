using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.IO.Files
{
    public interface IMrFileService
    {
        #region  - Upload FormFile   -
        Task<IEnumerable<string>> UploadMultiFilesAsync( MrCreationFileMode fileMode ,  Enum Catagory , IFormFileCollection files , string encyptionKey);
        Task<IEnumerable<string>> UploadMultiFilesAsync( MrCreationFileMode fileMode, Enum Catagory, IFormFileCollection files);

        Task<string> UploadFileAsync(MrCreationFileMode fileMode ,  Enum Catagory, IFormFile file , string encyptionKey); 
        Task<string> UploadFileAsync(MrCreationFileMode fileMode, Enum Catagory, IFormFile file ); 



        Task<IEnumerable<string>> UploadMultiFilesAsync(string path , IFormFileCollection files, MrCreationFileMode creationFileMode, string encyptionKey);
        Task<IEnumerable<string>> UploadMultiFilesAsync(string path , IFormFileCollection files, MrCreationFileMode creationFileMode);
        Task<string> UploadFileAsync(string path, IFormFile file, MrCreationFileMode creationFileMode);
        Task<string> UploadFileAsync(string path, IFormFile file, MrCreationFileMode creationFileMode, string encyptionKey);

        #endregion

        #region - Upload Base64file Files  -

        Task<IEnumerable<string>> UploadMultiFilesAsync(MrCreationFileMode fileMode, Enum Catagory, Base64FileCollection files ,string encyptionKey);
        Task<IEnumerable<string>> UploadMultiFilesAsync(MrCreationFileMode fileMode, Enum Catagory, Base64FileCollection files );
        Task<string> UploadFileAsync(MrCreationFileMode fileMode, Enum Catagory, Base64file file, string encyptionKey);
        Task<string> UploadFileAsync(MrCreationFileMode fileMode, Enum Catagory, Base64file file);



         Task<IEnumerable<string>> UploadMultiFilesAsync(string path, Base64FileCollection files, MrCreationFileMode CreationFileMode, string encyptionKey);
         Task<IEnumerable<string>> UploadMultiFilesAsync(string path, Base64FileCollection files, MrCreationFileMode CreationFileMode);

        Task<string> UploadFileAsync(string path, Base64file file, MrCreationFileMode CreationFileMode );
        Task<string> UploadFileAsync(string path, Base64file file, MrCreationFileMode CreationFileMode, string encyptionKey);

        #endregion


        Task<byte[]> GetFile(string filesPath);

        Task<byte[]> GetDecryptedFile(string dencyptionKey, string filePath);

        #region - Update Files -
        Task<string> UpdateFileAsync(UpdateBase64File fileInfo , string encyptionKey);


        Task<string> UpdateFileAsync(UpdateFormFile fileInfo ); 

        
        Task<IEnumerable<string>> UpdateMultiFilesAsync(IEnumerable<UpdateBase64File> files, string encyptionKey);

        Task<IEnumerable<string>> UpdateMultiFilesAsync(IEnumerable<UpdateFormFile> files);

        #endregion

        #region - Remove Files -
        bool RemoveFileAsync(string filePath);

        bool RemoveMultiFilesAsync(IEnumerable<string> filesPaths);


        #endregion


    }
}
