
using API_UPLOAD_DRIVE.DOMAIN.Entities;
using API_UPLOAD_DRIVE.DOMAIN.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Http;

namespace API_UPLOADD_DRIVE.INFRA.Storage;
public class GoogleCloudStorageService : IStorageService
{
    private readonly GoogleAuthorizationCodeFlow _authorization;
    public GoogleCloudStorageService(GoogleAuthorizationCodeFlow authorization)
    {
        _authorization = authorization; 
    }
    public string Upload(IFormFile file, User user)
    {
        var credential = new UserCredential(_authorization, user.Email, new TokenResponse
        {
            AccessToken = user.AccessToken,
            RefreshToken = user.RefreshToken,
        });
        var service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer
        {
            ApplicationName = "API-UPLOAD-TESTE",
            HttpClientInitializer = credential
        });
        var driveFile = new Google.Apis.Drive.v3.Data.File
        {
            Name = file.Name + "_API",
            MimeType = file.ContentType,
        };
        var command = service.Files.Create(driveFile, file.OpenReadStream(), file.ContentType);
        command.Fields = "id";
        var response = command.Upload();
        if(response.Status is not Google.Apis.Upload.UploadStatus.Completed)
        {
            throw response.Exception;
        }
        return command.ResponseBody.Id;
    }
}
