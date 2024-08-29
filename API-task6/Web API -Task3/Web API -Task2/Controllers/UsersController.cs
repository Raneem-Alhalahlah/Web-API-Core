using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
