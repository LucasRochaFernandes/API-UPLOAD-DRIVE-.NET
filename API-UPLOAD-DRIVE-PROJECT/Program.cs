
using API_UPLOAD_DRIVE.APP.UseCases.Users.UploadProfilePhoto;
using API_UPLOAD_DRIVE.DOMAIN.Storage;
using API_UPLOADD_DRIVE.INFRA.Storage;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;


namespace API_UPLOAD_DRIVE_PROJECT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

         
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUploadProfilePhotoUseCase, UploadProfilePhotoUseCase>();
            builder.Services.AddScoped<IStorageService>(options =>
            {
                var clientId = builder.Configuration.GetValue<string>("CloudStorage:ClientId");
                var clientSecret = builder.Configuration.GetValue<string>("CloudStorage:ClientSecret");
                var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new Google.Apis.Auth.OAuth2.ClientSecrets
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret
                    },
                    Scopes = [Google.Apis.Drive.v3.DriveService.Scope.Drive],
                    DataStore = new FileDataStore("API-UPLOAD-TESTE")
                }) ;
                return new GoogleCloudStorageService(apiCodeFlow);
            });

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
