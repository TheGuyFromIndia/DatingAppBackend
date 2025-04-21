using CloudinaryDotNet.Actions;

namespace DatingApp.Infrastructure.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile formFile);

        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}
