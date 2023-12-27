using Microsoft.AspNetCore.Mvc;
using ShopPhone.Application.Dto;
using ShopPhone.Application.Services;

namespace ShopPhone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment environment;

        public ProductController(IProductService productService, IWebHostEnvironment environment)
        {
            _productService = productService;
            this.environment = environment; 
        }
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile formFile, string product)
        {
            APIResponse response = new APIResponse();
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            List<string> Imageurl = new List<string>();

            try
            {
                string Filepath = GetFilepath(product);
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }

                string imagepath = Filepath + "\\" + product + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using (FileStream stream = System.IO.File.Create(imagepath))
                {
                    await formFile.CopyToAsync(stream);
                    response.ResponseCode = 200;
                    response.Result = "pass";
                    string _Imageurl = hosturl + "/Upload/" + product + "/" + product + ".png";
                    Imageurl.Add(_Imageurl);
                }
            }
            catch (Exception ex)
            {
                response.Errormessage = ex.Message;
            }
            return Ok(Imageurl);
        }
        [NonAction]
        private string GetFilepath(string product)
        {
            return this.environment.WebRootPath + "\\Upload\\" + product;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }
        [HttpGet("GetLatProduct")]
        public IActionResult GetLastProduct()
        {
            var products = _productService.GetAll();
            return Ok(products[products.Count - 1]);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public IActionResult Post(ProductDto product)
        {
            if (_productService.Add(product))
            {
                return CreatedAtAction("GetLastProduct", new { id = product.Id });
            }
            return Ok("Đã tồn tại");
        }
        [HttpPut("{id}")]
        public IActionResult Put(ProductDto product)
        {
            if (_productService.Update(product))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_productService.Delete(id))
            {
                return NoContent();
            }
            return NotFound("Không thể xóa vì không tồn tại");
        }
    }
}
