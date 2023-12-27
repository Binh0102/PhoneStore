using Microsoft.AspNetCore.Mvc;
using ShopPhone.Application.Dto;
using ShopPhone.Application.Services;

namespace ShopPhone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_categoryService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryService.Get(id);
            if(category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult Post(CategoryDto category)
        {
            if(_categoryService.Add(category))
            {
                return CreatedAtAction("GetCategory", new {id =category.Id}, category);
            }
            return Ok("Hãng đã tồn tại");
        }
        [HttpPut("{id}")]
        public IActionResult Put(CategoryDto category)
        {
            if (_categoryService.Update(category))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_categoryService.Delete(id))
            {
                return NoContent();
            }
            return NotFound("Không thể xóa vì hãng không tồn tại");
        }
    }
}
