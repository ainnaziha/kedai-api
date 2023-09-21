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
        [Route("checkout")]
        public IActionResult CheckOut()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(new Response { Status = false, Message = "Unauthorized access!" });
            }

            string orderNo = GenerateOrderNumber();

            Order order = new()
            {
                UserId = userId,
                OrderNo = orderNo,
            };

            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            return Ok(new Response { Status = true, Data = order });
        }

        private string GenerateOrderNumber()
        {
            int orderNumber = 1;

            var latestOrder = dBContext.Orders
                .OrderByDescending(o => o.Id)
                .FirstOrDefault();

            if (latestOrder != null)
            {
                string latestOrderNo = latestOrder.OrderNo;
                _ = int.TryParse(latestOrderNo.Split('_').LastOrDefault(), out orderNumber);
                orderNumber++;
            }

            return $"KAO_{orderNumber:D5}";
        }


        [HttpPut]
        [Route("{id}/complete")]
        public IActionResult CompleteOrder(int id, [FromBody] OrderRequest request)
        {
            var order = dBContext.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound(new Response { Status = false, Message = "Order not found!" });
            }

            order.Name = request.Name;
            order.Email = request.Email;
            order.Street = request.Street;
            order.Town = request.Town;
            order.InvoiceNo = request.InvoiceNo;
            order.Total = request.Total;
            order.IsPaid = true;

            if (request.CartIds != null && request.CartIds.Any())
            {
                var cartsToUpdate = dBContext.Carts.Where(c => request.CartIds.Contains(c.Id)).ToList();
                foreach (var cart in cartsToUpdate)
                {
                    cart.OrderId = order.Id;
                }
            }

            dBContext.SaveChanges();

            return Ok(new Response { Status = true, Message = $"Payment for order {order.OrderNo} is successful." });
        }


        [HttpGet]
        public IActionResult GetOrders()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(new Response { Status = false, Message = "Unauthorized access!" });
            }

            List<Order> orders = dBContext.Orders
                    .Where(o => o.UserId == userId && o.IsPaid)
                    .Include(o => o.Carts)
                    .ThenInclude(cart => cart.Product)                     
                    .ToList();

            return Ok(new Response { Status = true, Data = orders });
        }
    }
}
