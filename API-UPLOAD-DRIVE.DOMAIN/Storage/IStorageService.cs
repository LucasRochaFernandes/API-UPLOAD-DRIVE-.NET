using API_UPLOAD_DRIVE.DOMAIN.Entities;
using Microsoft.AspNetCore.Http;

namespace API_UPLOAD_DRIVE.DOMAIN.Storage;
public interface IStorageService
{
    string Upload(IFormFile file, User user);
}
