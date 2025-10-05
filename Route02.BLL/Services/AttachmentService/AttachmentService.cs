using Microsoft.AspNetCore.Http;
using Route02.BLL.Services.AttachmentService.Interface;

namespace Route02.BLL.Services.AttachmentService;

public class AttachmentService: IAttachmentService
{
    private const int MaxSize = 2_097_152; // 2 MB

    private static List <string> AllowedExtension = [".png", ".jpg", ".jpeg"];

    /// <summary>
    ///     - File Parameter That Is The File Uploaded From User
    ///     - Folder Name That Is Place Storage It File (Image)
    /// </summary>
    /// <param name="file"></param>
    /// <param name="folderName"></param>
    /// <returns></returns>
    public string? Upload(IFormFile file, string folderName)
    {
        // ✅ Check The File Is Valid Or Invalid
        if (file.Length == 0 || file.Length > MaxSize) return null;

        /// 1- Check Extension
        // Catch Extension From File Uploaded
        // var extension = Path.GetExtension(file.FileName);

        // ✅ ToLowerInvariant() => To Ignore Small Letter Or Capital Letter ["jpeg", "Jpeg"]
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        // Check If The File Extension, Not Equal The Available Extensions Return Null
        if (!AllowedExtension.Contains(extension)) return null;

        /// 2- 👆 Check Size
        // Check If The File Size, Equal 0 Or Greater Than Max Size Return Null
        // if (file.Length > MaxSize) return null;

        /// 3- Get Located Folder Path
        // GetCurrentDirectory() => Gets The Current Working Directory Of The Application (Stop On Solution)
        // var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

        // It Is Preferred Way To Work Any Operating System [Windows - macOS - Linux]
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName);

        // ✅ Check If The Folder Path Is Not Found Go To Create It
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        /// 4- Make Attachment Name Unique
        // Using Guid
        var fileName = $"{ Guid.NewGuid() }_{file.FileName}";

        /// 5- Git File Path
        // Actually Path Storage
        var filePath = Path.Combine(folderPath, fileName);

        /// 6- Create File Stream To File [Unmanaged]
        // Open Connection With FileStream
        using FileStream fileStream = new FileStream(filePath, FileMode.Create);

        /// 7- Use Stream To Copy File
        // CopyTo(fileStream) => It Is Actually Storage File In Server - Save It In Hard Disk [DB]
        file.CopyTo(fileStream);

        /// 8- Return File Name To Store In Database
        //
        return fileName;
    }

    public bool Delete(string filePath)
    {
        // ✅ Make sure path is valid
        if (string.IsNullOrEmpty(filePath)) return false;

        // Check If The File Not Found Return False
        if (!File.Exists(filePath)) return false;

        File.Delete(filePath);

        return true;
    }
}