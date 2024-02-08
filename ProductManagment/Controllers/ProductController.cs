using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController() { }

        [HttpGet]
        public IActionResult Get()
        {

            return Ok();
        }

        [HttpPost]
        public IActionResult Save()
        {

            return Ok();
        }

        [HttpPut]
        public IActionResult Update()
        {

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {

            return Ok();
        }
    }
}
