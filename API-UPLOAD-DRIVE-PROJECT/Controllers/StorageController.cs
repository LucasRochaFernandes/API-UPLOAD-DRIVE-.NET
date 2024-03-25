using API_UPLOAD_DRIVE.APP.UseCases.Users.UploadProfilePhoto;
using Microsoft.AspNetCore.Mvc;

namespace API_UPLOAD_DRIVE_PROJECT.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        [HttpPost]
        public IActionResult UploadImage([FromServices] IUploadProfilePhotoUseCase useCase,IFormFile file)
        {
        useCase.Execute(file);
        return Created();
        }
    }

