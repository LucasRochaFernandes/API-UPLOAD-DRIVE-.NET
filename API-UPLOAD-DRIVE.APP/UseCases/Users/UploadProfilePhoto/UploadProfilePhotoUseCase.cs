using API_UPLOAD_DRIVE.DOMAIN.Entities;
using API_UPLOAD_DRIVE.DOMAIN.Storage;
using FileTypeChecker;
using Microsoft.AspNetCore.Http;

namespace API_UPLOAD_DRIVE.APP.UseCases.Users.UploadProfilePhoto;
public class UploadProfilePhotoUseCase : IUploadProfilePhotoUseCase
{
    private readonly IStorageService _storageService;
    public UploadProfilePhotoUseCase(IStorageService storageService)
    {
        _storageService = storageService;
    } 
    public void Execute(IFormFile file)
    {
        var streamFile = file.OpenReadStream();
        var isRecognizableType = FileTypeValidator.IsImage(streamFile);
        if(isRecognizableType == false)
        {
            throw new Exception("The File Is Not An Image.");
        }
        _storageService.Upload(file, GetFromDatabase());

    }
    private User GetFromDatabase()
    {
        return new User
        {
            Id = 1,
            Email = "teste@gmail.com",
            Name = "Teste",
            RefreshToken = "Teste",
            AccessToken= "Teste"
        };
    }
}
