namespace Meteors.AspNetCore.IO.Files
{
    /// <summary>
    /// create file modes
    /// </summary>
    public enum MrCreationFileMode
    {
        Normal = 0,

        Compress = 1,
    }
    /// <summary>
    /// Encryption file modes 
    /// </summary>
    public enum MrEncryptionMode
    {
        Encryption = 1,

        Decryption = 2
    }
}
