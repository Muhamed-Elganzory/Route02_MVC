using Microsoft.AspNetCore.Http;

namespace Route02.BLL.Services.AttachmentService.Interface;

public interface IAttachmentService
{
    public string? Upload (IFormFile formFile, string folderName);

    public bool Delete(string filePath);
}