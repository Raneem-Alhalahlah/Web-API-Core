using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API__Task2.DTOs;
using Web_API__Task2.Models;

namespace Web_API__Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;

        public UsersController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }


        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _myDbContext.Users.ToList();
            return Ok(users);
        }


        [HttpGet]
        [Route("GetUserById/{id:int:min(10)}")]
        public IActionResult GetUserById(int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var user = _myDbContext.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpGet]
        [Route("GetUserByName/{name}")]
        public IActionResult GetUserByName(string name)
        {
            var user = _myDbContext.Users
               .FirstOrDefault(c => c.Username == name);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var user = _myDbContext.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            _myDbContext.Users.Remove(user);
            _myDbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public IActionResult Adduser([FromForm] UserRequest userOTD)
        {

            if (userOTD == null)
            {
                return BadRequest("not valid");
            }

            var y = new User
            {
                Username = userOTD.Username,

                Password = userOTD.Password,
                Email = userOTD.Email,
            
            };
            _myDbContext.Users.Add(y);
            _myDbContext.SaveChanges();

            return Ok("Category added successfully!");


        }


        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromForm] UserRequest userOTD)
        {


            var existinguser = _myDbContext.Users.FirstOrDefault(x => x.UserId == id);

            if (existinguser == null)
            {

                return NotFound();
            }


            existinguser.Username = userOTD.Username;
            existinguser.Password = userOTD.Password;

                
            existinguser.Email= userOTD.Email;
           



            _myDbContext.Update(existinguser);
            _myDbContext.SaveChanges();
            return Ok();







        }



    }
}
