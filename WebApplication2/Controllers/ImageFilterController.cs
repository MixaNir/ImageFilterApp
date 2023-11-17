using Microsoft.AspNetCore.Mvc;
using WebApplication2.ImageFilters.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageFilterController : ControllerBase
    {
        private readonly ILogger<ImageFilterController> _logger;
        public static IWebHostEnvironment _environment;

        public ImageFilterController(ILogger<ImageFilterController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] FileFilterData dataObj)
        {
            try
            {
                List<IFilter> filters = new List<IFilter>();
                if (dataObj != null && dataObj.filtersEnums.Length > 0)
                {
                    if (dataObj.filtersEnums.Contains(FiltersEnum.ResizeModels))
                    {
                        filters.Add(ImageWorker.PrepareResizeModel(new System.Drawing.Size(dataObj.width, dataObj.height)));
                    }

                    ImageWorker.ApplyFilters(dataObj.ImgID, filters);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("FileFilterData error: " + ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok(dataObj);
        }

    }
}
