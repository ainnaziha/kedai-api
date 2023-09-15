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

            //todo: create order no starting with KAO_00001
            //todo: save order no, userId, cartId, total to orders table
            //return order as response
            Order order = null;

            return Ok(new Response { Status = true, Data = order });
        }

        [HttpPut]
        [Route("{order_id}/complete")]
        public IActionResult CompleteOrder([FromBody] CompleteOrderRequest request)
        {
            //todo: check order id if exist
            //todo: update name, email, street, town and invoice no if any

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

           //todo: get orders based on user id, only completed orders

            return Ok(new Response { Status = true });
        }
    }
}
