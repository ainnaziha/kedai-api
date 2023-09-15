using KedaiAPI.Data;
using KedaiAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KedaiAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDBContext dBContext;

        public OrderController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpPost]
        [Route("create-order-no")]
        public IActionResult CreateOrderNo([FromBody] OrderNoRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(new Response { Status = false, Message = "Unauthorized access!" });
            }

            string newOrderNo = GenerateOrderNumber();

            var order = new Order
            {
                UserId = userId,
                OrderNo = newOrderNo,
                CartId = request.CartId,
                Total = request.Total
            };

            dbContext.Orders.Add(order);
            dbContext.SaveChanges();

            return Ok(new Response { Status = true, Data = order });
        }

        private string GenerateOrderNumber()
        {
            string latestOrderNo = _dbContext.Orders.OrderByDescending(o => o.Id).Select(o => o.OrderNo).FirstOrDefault();
            int orderNumber = int.Parse(latestOrderNo?.Split('_').LastOrDefault() ?? "0") + 1;
            return $"KAO_{orderNumber:D5}";
        }

        [HttpPut]
        [Route("{order_id}/complete")]
        public IActionResult CompleteOrder(int order_id, [FromBody] CompleteOrderRequest request)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == order_id);

            if (order == null)
            {
                return NotFound(new Response { Status = false, Message = "Order not found!" });
            }

            order.Name = request.Name;
            order.Email = request.Email;
            order.Street = request.Street;
            order.Town = request.Town;
            order.InvoiceNo = request.InvoiceNo;

            dbContext.SaveChanges();

            return Ok(new Response { Status = true });
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(new Response { Status = false, Message = "Unauthorized access!" });
            }

            var completedOrders = _dbContext.Orders
                    .Where(o => o.UserId == userId && o.IsCompleted)
                    .Include(o => o.Cart.CartItems)
                    .ToList();

            return Ok(new Response { Status = true, Data = completedOrders });
        }
    }
}
