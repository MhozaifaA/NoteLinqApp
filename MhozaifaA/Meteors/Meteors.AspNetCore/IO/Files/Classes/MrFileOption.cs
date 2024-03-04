using Meteors.AspNetCore.IO.Files;
using System;
using System.Collections.Generic;
using System.IO;
namespace Meteors.AspNetCore.IO.Files
{
    public class MrFileOption
    {
        internal string Path { get; set; } = String.Empty;

        internal MrFileType Type { get; set; } = MrFileType.Documents;

        internal string FinalFilePath => System.IO.Path.Combine(Path, Type.ToString());
        /// <summary>
        ///  list of files Categories 
        /// </summary>
        internal IEnumerable<string> Categories { get; set; } 

    }
}
