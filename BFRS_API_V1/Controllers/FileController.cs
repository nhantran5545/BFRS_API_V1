using BusinessObjects.IService;
using BusinessObjects.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadImage([FromForm] FileRequest fileRequest)
        {

            var imageUrl = await _fileService.Upload(fileRequest);
            return Ok(new { imageUrl });

        }

        [HttpGet]
        public async Task<IActionResult> DownloadImage(string name)
        { 
                var imageFileStream = await _fileService.Get(name);
                string fileType = "jpeg";
                if (name.Contains("png"))
                {
                    fileType = "png";
                }
                return File(imageFileStream, $"image/{fileType}", $"blobfile.{fileType}");
        }
    }
}
