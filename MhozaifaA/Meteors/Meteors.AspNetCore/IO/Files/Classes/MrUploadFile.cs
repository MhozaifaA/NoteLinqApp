using Meteors.AspNetCore.IO.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Meteors.AspNetCore.IO.Files
{

    #region - update Base64file -
    public class UpdateBase64File : FileInfo
    {
        public Base64file File { get; set; }
    }
    #endregion

    #region  - update Form file - 
    public class UpdateFormFile : FileInfo
    {
        public IFormFile File { get; set; }

    }

    #endregion


    #region  - Basic objects -
    public class Base64file
    {
        public string File { get; set; }

        public string FileName { get; set; }

    }

    public class Base64FileCollection : IEnumerable<Base64file>
    {
        public List<Base64file> Bases64List { get; set; }
        public Base64FileCollection()
        {
            Bases64List = new List<Base64file>();
        }
        public IEnumerator<Base64file> GetEnumerator()
        {
            foreach (var Base64file in Bases64List)
            {
                yield return Base64file;
            }
        }
        public Base64FileCollection AddBase64file(Base64file Base64file)
        {
            Bases64List.Add(Base64file);
            return this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public class FileInfo
    {
        public MrCreationFileMode FileMode { get; set; }

        public Enum Catagory { get; set; }

        public string OldFileUrl { get; set; }
    }

    public class EncryptionInfo
    {
        public string EncyptionKey { get; set; }
    }


    public class GetDecryptedFile
    {
        public string EncyptionKey { get; set; }

        public string FilePath { get; set; }
    }
    #endregion

}
