using E_Commerce.Connection;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;

namespace E_Commerce.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        DbContextConnection Context;
        public UserController(DbContextConnection _context)
        {
            Context = _context;
        }



        [HttpGet]
        public IActionResult Display()
        {
            List<User> deptlist = Context.Users.ToList();
            return Ok(deptlist);
        }
        [HttpGet]
        [Route("{id:int}")]//api/User/num_id
        public IActionResult Getbyid(int id)
        {
            User user = Context.Users.FirstOrDefault(i => i.Id == id);
            return Ok(user);
        }
        [HttpGet]
        [Route("{name:alpha}")]//api/User/name
        public IActionResult Getbyname(string name)
        {
            User user = Context.Users.FirstOrDefault(i => i.Name == name);
            return Ok(user);
        }
        [HttpPost]
        public IActionResult Add(User User)
        {
            Context.Users.Add(User);
            Context.SaveChanges();
            return CreatedAtAction("Getbyid", new { id = User.Id }, User);
        }
        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, User userfromrequest)
        {
            User userfromdb = Context.Users.SingleOrDefault(i => i.Id == id);
            if (userfromdb != null)
            {
                userfromdb.Name = userfromrequest.Name;
                userfromdb.Email = userfromrequest.Email;
                userfromdb.Password = userfromrequest.Password;
                Context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound("User Not Valid");
            }
        }
        //[HttpGet("Count")]
        //public ActionResult<List<Deptdto>> getdetalisdto()
        //{
        //    List<Department> dept = Context.Department.Include(i => i.Employee).ToList();
        //    List<Deptdto> deptlistdto = new List<Deptdto>();
        //    foreach (Department item in dept)
        //    {
        //        Deptdto dto = new Deptdto();
        //        dto.Id = item.id;
        //        dto.Name = item.name;
        //        dto.Emp_count = item.Employee.Count();
        //        deptlistdto.Add(dto);
        //    }

        //    return deptlistdto;
        //    //return Ok(deptlistdto); IActionResult
        //}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            User user = Context.Users.SingleOrDefault(i => i.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            Context.Users.Remove(user);
            Context.SaveChanges();
            return Ok();
        }
    }
}
