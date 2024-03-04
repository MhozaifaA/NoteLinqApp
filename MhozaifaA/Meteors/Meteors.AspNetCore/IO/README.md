# IMrFileService

> Namespace: Meteors.AspNetCore.IO.Files

Service for Multiple operations with files (upload - delete - update)

####  **Support** 

- Work with Multiple Files in each Operation
- Compression & DeCompression options
- Encryption & Decryption Mode
- Support FormFile and Base64 formulas

------



## Get Started

**Startup.cs**

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMrFileService(
                 option => option.MrFileOption( path: "wwwroot" , MrFileType:                              MrFileType.Documents).
                 MrFileCatagories(typeof(FilesCetagory)));

}   
//or with default configration values
public void ConfigureServices(IServiceCollection services)
{
    services.AddMrFileService();
}
```

#### **Explain **MrFileOption Parameters (they`ar Optional)

* path : config base path for saving uploaded files .
* MrFileType :  config uploaded files type (Documents , Images ) by default (Documents) .
* MrFileCatagories take Enum type which is files catagory .

## MrFileController

>         - Support specific route to decompress files  "[Mrfile/GetDecompressFiles?filePath=](Mrfile/GetDecompressFiles?filePath=){The path of the file that needs to be decompressed}"
>         - Support specific path to decrypt files "[Mrfile/GettDecryptedFiles?filePath=](Mrfile/GettDecryptedFiles?filePath=){The path of the file that needs to be Decrypted}[&passKey=](){Decryption algorithm key}" 

------

# `more description  coming  soon`