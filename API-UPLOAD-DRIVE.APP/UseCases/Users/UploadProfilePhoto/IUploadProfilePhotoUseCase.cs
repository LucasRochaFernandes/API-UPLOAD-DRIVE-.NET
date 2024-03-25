using Microsoft.AspNetCore.Http;

namespace API_UPLOAD_DRIVE.APP.UseCases.Users.UploadProfilePhoto;
public interface IUploadProfilePhotoUseCase
{
    public void Execute(IFormFile file);
}
