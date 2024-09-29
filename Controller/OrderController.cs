using E_Commerce.Connection;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        DbContextConnection Context;
        public OrderController(DbContextConnection _context)
        {
            Context = _context;
        }



        [HttpGet]
        public IActionResult Display()
        {
            List<Order> oredr = Context.Orders.ToList();
            return Ok(oredr);
        }
        [HttpGet]
        [Route("{id:int}")]//api/order/num_id
        public IActionResult Getbyid(int id)
        {
            Order oredr = Context.Orders.FirstOrDefault(i => i.Id == id);
            return Ok(oredr);
        }
        //[HttpGet]
        //[Route("{name:alpha}")]//api/Cart/name
        //public IActionResult Getbyname(string name)
        //{
        //    Cart pro = Context.Carts.FirstOrDefault(i => i.Name == name);
        //    return Ok(pro);
        //}
        [HttpPost]
        public IActionResult Add(Order oredr)
        {
            Context.Orders.Add(oredr);
            Context.SaveChanges();
            return CreatedAtAction("GetById", new { id = oredr.Id }, oredr);
        }

        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Order orderfromrequest)
        {
            Order orderfromdb = Context.Orders.SingleOrDefault(i => i.Id == id);
            if (orderfromdb != null)
            {
                orderfromdb.OrderDate = orderfromrequest.OrderDate;
                orderfromdb.UserId = orderfromrequest.UserId;
                orderfromdb.OrderItems = orderfromrequest.OrderItems;
                orderfromdb.User = orderfromrequest.User;

                Context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound("order Not Valid");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            Order order = Context.Orders.SingleOrDefault(i => i.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            Context.Orders.Remove(order);
            Context.SaveChanges();
            return Ok();
        }
    }
}
