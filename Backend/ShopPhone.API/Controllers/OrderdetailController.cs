using Microsoft.AspNetCore.Mvc;
using ShopPhone.Application.Dto;
using ShopPhone.Application.Services;

namespace ShopPhone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderdetailController : ControllerBase
    {
        private readonly IOrderdetailService _orderdetailService;
        public OrderdetailController(IOrderdetailService orderdetailService)
        {
            _orderdetailService = orderdetailService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orderdetailService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var orderdetail = _orderdetailService.Get(id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            return Ok(orderdetail);
        }
        [HttpPost]
        public IActionResult Post(OrderdetailDto orderdetail)
        {
            if (_orderdetailService.Add(orderdetail))
            {
                return CreatedAtAction("GetOrderdetail", new { id = orderdetail.Id }, orderdetail);
            }
            return Ok("Hãng đã tồn tại");
        }
        [HttpPut("{id}")]
        public IActionResult Put(OrderdetailDto orderdetail)
        {
            if (_orderdetailService.Update(orderdetail))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_orderdetailService.Delete(id))
            {
                return NoContent();
            }
            return NotFound("Không thể cóa vì hãng không tồn tại");
        }
    }
}
