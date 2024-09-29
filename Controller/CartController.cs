using E_Commerce.Connection;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        DbContextConnection Context;
        public CartController(DbContextConnection _context)
        {
            Context = _context;
        }



        [HttpGet]
        public IActionResult Display()
        {
            List<Cart> cart = Context.Carts.ToList();
            return Ok(cart);
        }
        [HttpGet]
        [Route("{id:int}")]//api/Cart/num_id
        public IActionResult Getbyid(int id)
        {
            Cart cart = Context.Carts.FirstOrDefault(i => i.Id == id);
            return Ok(cart);
        }
        //[HttpGet]
        //[Route("{name:alpha}")]//api/Cart/name
        //public IActionResult Getbyname(string name)
        //{
        //    Cart pro = Context.Carts.FirstOrDefault(i => i.Name == name);
        //    return Ok(pro);
        //}
        [HttpPost]
        public IActionResult Add(Cart cart)
        {
            Context.Carts.Add(cart);
            Context.SaveChanges();
            return CreatedAtAction("GetById", new { id = cart.Id }, cart);
        }

        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Cart cartfromrequest)
        {
            Cart cartfromdb = Context.Carts.SingleOrDefault(i => i.Id == id);
            if (cartfromdb != null)
            {
                cartfromdb.UserID = cartfromrequest.UserID;
                cartfromdb.Users = cartfromrequest.Users;
                cartfromdb.CartItems = cartfromrequest.CartItems;
                Context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound("Cart Not Valid");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            Cart cart = Context.Carts.SingleOrDefault(i => i.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            Context.Carts.Remove(cart);
            Context.SaveChanges();
            return Ok();
        }
    }
}
