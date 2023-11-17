using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageUploadController : ControllerBase
    {
        private readonly ILogger<ImageUploadController> _logger;
        public static IWebHostEnvironment _environment;

        public ImageUploadController(ILogger<ImageUploadController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] FileUploadData objFile)
        {
            try
            {
                if (objFile.file.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.file.FileName))
                    {
                        objFile.file.CopyTo(filestream);
                        filestream.Flush();
                    }
                }
                objFile.ImgName = _environment.WebRootPath + "\\Upload\\" + objFile.file.FileName;
                ImageWorker.AddImageInfo(objFile);
            }
            catch (Exception ex)
            {
                _logger.LogError("ImageUploadController error: " + ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok(objFile);
        }
    }
}
