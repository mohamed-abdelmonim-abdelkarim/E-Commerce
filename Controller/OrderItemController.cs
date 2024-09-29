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
    public class OrderItemController : ControllerBase
    {
        private readonly DbContextConnection _context;

        public OrderItemController(DbContextConnection context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Display()
        {
            List<OrderItem> orderItems = _context.OrderItems
                .Include(oi => oi.Product) 
                .Include(oi => oi.Order) 
                .ToList();
            return Ok(orderItems);
        }

        
        [HttpGet("{id:int}")] // api/OrderItem/num_id
        public IActionResult GetById(int id)
        {
            OrderItem orderItem = _context.OrderItems
                .Include(oi => oi.Product) 
                .Include(oi => oi.Order)
                .FirstOrDefault(oi => oi.Id == id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }
        [HttpPost]
        public IActionResult Add(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
            return CreatedAtAction("GetById", new { id = orderItem.Id }, orderItem);
        }
        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, OrderItem orderItemFromRequest)
        {
            OrderItem orderItemFromDb = _context.OrderItems.SingleOrDefault(oi => oi.Id == id);
            if (orderItemFromDb != null)
            {
                orderItemFromDb.Quantity = orderItemFromRequest.Quantity;
                orderItemFromDb.ProductId = orderItemFromRequest.ProductId;
                orderItemFromDb.OrderId = orderItemFromRequest.OrderId; 
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound("OrderItem Not Valid");
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOrderItem(int id)
        {
            OrderItem orderItem = _context.OrderItems.SingleOrDefault(oi => oi.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            _context.SaveChanges();
            return Ok();
        }
    }
}
