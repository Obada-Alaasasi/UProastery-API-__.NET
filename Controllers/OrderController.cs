using Microsoft.AspNetCore.Mvc;
using UProastery.Contracts;
using UProastery.Data;

namespace UProastery.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase {

        private readonly IOrderManager _orderManager;
        public OrderController(IOrderManager order_manager)
        {
            this._orderManager = order_manager;
        }

        [Route("add")]
        [HttpPost]
        public async Task<ActionResult<string>> AddOrder(List<OrderItemDTO> dto_in) {
            
            if(!dto_in.Any()) {
                return BadRequest("Error: Empty Input!");
            }

            int added = await _orderManager.AddOrderItems(dto_in);
            if (added > 0) {
                return Ok($"Order with {added} items added successfully");
            }
            else return Ok("No items were added!");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO_OUT>> GetOrder(int id) {

            if (id == default) {
                return BadRequest("Please Enter an id!");
            }
            OrderDTO_OUT order = await _orderManager.GetOrder(id);
            if (order == null) {
                return BadRequest($"No order exists with Id {id}");
            }

            return order;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteOrder(int id) {
            if(id == default) {
                return BadRequest();
            }
            int result = await _orderManager.DeleteOrder(id);
            if (result == 0) {
                return NotFound();
            }
            else return Ok(result);
        }

    }
}
