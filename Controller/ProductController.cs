using DocumentFormat.OpenXml.Spreadsheet;
using E_Commerce.Connection;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unipluss.Sign.ExternalContract.Entities;

namespace E_Commerce.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        DbContextConnection Context;
        public ProductController(DbContextConnection _context)
        {
            Context = _context;
        }



        [HttpGet]
        public IActionResult Display()
        {
            List<Product> pro = Context.Products.ToList();
            return Ok(pro);
        }
        [HttpGet]
        [Route("{id:int}")]//api/Product/num_id
        public IActionResult Getbyid(int id)
        {
            Product pro = Context.Products.FirstOrDefault(i => i.Id == id);
            return Ok(pro);
        }
        [HttpGet]
        [Route("{name:alpha}")]//api/Product/name
        public IActionResult Getbyname(string name)
        {
            Product pro = Context.Products.FirstOrDefault(i => i.Name == name);
            return Ok(pro);
        }
        [HttpPost]
        public IActionResult Add(Product pro)
        {
            Context.Products.Add(pro);
            Context.SaveChanges();
            return CreatedAtAction("GetById", new { id = pro.Id }, pro);
        }

        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Product profromrequest)
        {
            Product profromdb = Context.Products.SingleOrDefault(i => i.Id == id);
            if (profromdb != null)
            {
                profromdb.Name = profromrequest.Name;
                profromdb.Description = profromrequest.Description;
                profromdb.Price = profromrequest.Price;
                profromdb.imageURL = profromrequest.imageURL;
                Context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound("Pro Not Valid");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            Product product = Context.Products.SingleOrDefault(i => i.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            Context.Products.Remove(product);
            Context.SaveChanges();
            return Ok();
        }
    }
}
