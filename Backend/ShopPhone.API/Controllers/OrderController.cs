using Microsoft.AspNetCore.Mvc;
using ShopPhone.Application.Dto;
using ShopPhone.Application.Services;

namespace ShopPhone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orderService.GetAll());
        }
        [HttpGet("GetLastOrders")]
        public IActionResult GetLastProduct()
        {
            var orders = _orderService.GetAll();
            return Ok(orders[orders.Count - 1]);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orderService.Get(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpPost]
        public IActionResult Post(OrderDto order)
        {
            if (_orderService.Add(order))
            {
                return CreatedAtAction("GetLastOrders", new { id = order.Id }, order);
            }
            return Ok("Đã tồn tại");
        }
        [HttpPut("{id}")]
        public IActionResult Put(OrderDto order)
        {
            if (_orderService.Update(order))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_orderService.Delete(id))
            {
                return NoContent();
            }
            return NotFound("Không thể xóa vì không tồn tại");
        }
    }
}
