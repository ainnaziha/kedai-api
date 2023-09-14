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

            var cart = GetOrCreateCart(userId);

            var total = CalculateTotal(cart);
            var cartItems = GetCartItems(cart);

            var response = new CartResponse
            {
                Id = cart.Id,
                Total = string.Format("RM {0:0.00}", total),
                Items = cartItems
            };

            return Ok(new Response { Status = true, Data = response });
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

            var cart = GetOrCreateCart(userId);

            var product = dBContext.Products.FirstOrDefault(p => p.Id == request.ProductId);

            if (product == null)
            {
                return NotFound(new Response { Status = false, Message = "Product not found!" });
            }

            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId && !ci.IsDeleted);

            if (existingCartItem != null)
            {
                existingCartItem.Quantities++;
            }
            else
            {
                var newCartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = request.ProductId,
                    Quantities = 1,
                    IsDeleted = false
                };

                dBContext.CartItems.Add(newCartItem);
            }

            dBContext.SaveChanges();

            var total = CalculateTotal(cart);
            var cartItems = GetCartItems(cart);

            var response = new CartResponse
            {
                Id = cart.Id,
                Total = string.Format("RM {0:0.00}", total),
                Items = cartItems
            };

            return Ok(new Response { Status = true, Data = response });
        }

        private Cart GetOrCreateCart(string userId)
        {
            var existingCart = dBContext.Carts
                .Include(cart => cart.CartItems)
                .ThenInclude(cartItem => cartItem.Product)
                .FirstOrDefault(c => c.UserId == userId && !c.IsCompleted);

            if (existingCart != null)
            {
                return existingCart;
            }

            var newCart = new Cart
            {
                UserId = userId,
                IsCompleted = false
            };

            dBContext.Carts.Add(newCart);
            dBContext.SaveChanges();

            return newCart;
        }

        private double CalculateTotal(Cart cart)
        {
            return cart.CartItems
                .Where(ci => !ci.IsDeleted)
                .Sum(ci => ci.Product.Price * ci.Quantities);
        }

        private List<CartItemResponse> GetCartItems(Cart cart)
        {
            return cart.CartItems
                .Where(ci => !ci.IsDeleted)
                .Select(ci => new CartItemResponse
                {
                    Id = ci.Id,
                    Product = ci.Product,
                    Quantity = ci.Quantities,
                    Subtotal = string.Format("RM {0:0.00}", ci.Product.Price * ci.Quantities),
                }).ToList();
        }
    }
}
