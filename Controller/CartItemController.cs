using DocumentFormat.OpenXml.Spreadsheet;
using E_Commerce.Connection;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace E_Commerce.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly DbContextConnection _context;

        public CartItemController(DbContextConnection context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Display()
        {
            List<CartItem> cartItems = _context.CartItems
                .Include(ci => ci.Product) 
                .ToList();
            return Ok(cartItems);
        }

        
        [HttpGet("{id:int}")] // api/CartItem/num_id
        public IActionResult GetById(int id)
        {
            CartItem cartItem = _context.CartItems
                .Include(ci => ci.Product) 
                .FirstOrDefault(ci => ci.Id == id);

            if (cartItem == null)
            {
                return NotFound();
            }

            return Ok(cartItem);
        }
        [HttpPost]
        public IActionResult Add(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
            return CreatedAtAction("GetById", new { id = cartItem.Id }, cartItem);
        }
        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, CartItem cartItemFromRequest)
        {
            CartItem cartItemFromDb = _context.CartItems.SingleOrDefault(ci => ci.Id == id);
            if (cartItemFromDb != null)
            {
                cartItemFromDb.Quantity = cartItemFromRequest.Quantity;
                cartItemFromDb.ProductId = cartItemFromRequest.ProductId;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound("CartItem Not Valid");
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCartItem(int id)
        {
            CartItem cartItem = _context.CartItems.SingleOrDefault(ci => ci.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
            return Ok();
        }
    }
}

