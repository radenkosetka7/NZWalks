using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("upload")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO requestDTO)
        {
            ValidateFileUpload(requestDTO);
            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = requestDTO.File,
                    FileExtension = Path.GetExtension(requestDTO.File.FileName),
                    FileSizeInBytes = requestDTO.File.Length,
                    FileName = requestDTO.FileName,
                    FileDescription = requestDTO.FileDescription
                };
                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO requestDTO)
        {
            var allowedExtension = new[] {".jpg", ".jpeg", ".png" };
            if(!allowedExtension.Contains(Path.GetExtension(requestDTO.File.FileName)))
            {
                ModelState.AddModelError("File", "Invalid file type");
            }

            if(requestDTO.File.Length >10485760)
            { 
                ModelState.AddModelError("File", "File size is too large");
            }
        }
    }
}
