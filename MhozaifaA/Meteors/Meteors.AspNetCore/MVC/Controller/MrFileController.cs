using Meteors.AspNetCore.IO.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Controller
{
    //[Route("static/[controller]/[action]")]
    public class MrFileController : ControllerBase
    {
        public readonly IMrFileService MrFileService;
        public MrFileController(IMrFileService MrFileService )
        {
            this.MrFileService = MrFileService;
        }


        public async Task<IActionResult> GetDecompressFiles(string filePath)
        {
            return File(await MrFileService.GetFile(filePath) , "application/octet-stream" , filePath.GetFileNameFromPath());
        }
        
        
        public async Task<IActionResult> GetDecryptedFiles(
           string filePath , string passKey)
        {
            return File(await MrFileService.GetDecryptedFile(passKey,filePath), "application/octet-stream", filePath.GetFileNameFromPath());
        }

    }
}
