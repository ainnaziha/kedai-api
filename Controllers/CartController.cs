using KedaiAPI.Data;
using KedaiAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KedaiAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDBContext dBContext;

        public CartController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet]
        public IActionResult GetCarts()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(new Response { Status = false, Message = "Unauthorized access!" });
            }

            CartResponse response = GetCartResponse(userId);

            return Ok(new Response { Status = true, Data = response });
        }

        [HttpGet]
        [Route("total")]
        public IActionResult GetCartTotal()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(new Response { Status = false, Message = "Unauthorized access!" });
            }

            int cartCount = dBContext.Carts
                .Where(c => c.UserId == userId && !c.IsDeleted)
                .Count();

            return Ok(new Response { Status = true, Data = cartCount });
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddToCart([FromBody] CartRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(new Response { Status = false, Message = "Unauthorized access!" });
            }

            Cart? cart = dBContext.Carts
                .FirstOrDefault(c => c.UserId == userId && !c.IsDeleted && c.ProductId == request.ProductId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    ProductId = request.ProductId ?? 0,
                    Quantity = 1,
                    IsDeleted = false,
                    OrderId = null,
                };

                dBContext.Carts.Add(cart);
            }
            else
            {
                cart.Quantity++;
            }

            dBContext.SaveChanges();

            CartResponse response = GetCartResponse(userId);

            return Ok(new Response { Status = true, Data = response });
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateCartItem([FromBody] CartRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(new Response { Status = false, Message = "Unauthorized access!" });
            }

            Cart? cart = dBContext.Carts
                .FirstOrDefault(c => c.Id == request.CartId && c.UserId == userId && !c.IsDeleted);

            if (cart == null)
            {
                return NotFound(new Response { Status = false, Message = "Cart not found!" });
            }

            cart.Quantity = request.Quantity ?? 0;
            cart.IsDeleted = request.Quantity == 0;

            dBContext.SaveChanges();

            CartResponse response = GetCartResponse(userId);

            return Ok(new Response { Status = true, Data = response });
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(new Response { Status = false, Message = "Unauthorized access!" });
            }

            List<Cart> cartsToDelete = dBContext.Carts
                .Where(c => c.UserId == userId && !c.IsDeleted)
                .ToList();

            foreach (Cart cart in cartsToDelete)
            {
                cart.IsDeleted = true;
            }

            dBContext.SaveChanges();

            return Ok(new Response
            {
                Status = true,
                Message = "Cart emptied successfully!",
                Data = new CartResponse
                {
                    Total = "0.00",
                    TotalFormatted = "RM 0.00",
                    Items = new List<CartItemResponse>()
                }
            }); ; ;
        }

        private CartResponse GetCartResponse(string userId)
        {
            List<Cart> carts = dBContext.Carts
              .Include(c => c.Product)
              .Where(c => c.UserId == userId && !c.IsDeleted)
              .ToList();

            List<CartItemResponse> items = carts.SelectMany(cart => new List<CartItemResponse>
            {
                new CartItemResponse
                {
                    Id = cart.Id,
                    Product = cart.Product,
                    Quantity = cart.Quantity,
                    Subtotal = string.Format("RM {0:0.00}", cart.Product.Price * cart.Quantity),
                }
            }).ToList();

            return new CartResponse {
                Total = string.Format("{0:0.00}", carts.Sum(ci => ci.Product.Price * ci.Quantity)),
                TotalFormatted = string.Format("RM {0:0.00}", carts.Sum(ci => ci.Product.Price * ci.Quantity)),
                Items = items,
            };
        }
    }
}
