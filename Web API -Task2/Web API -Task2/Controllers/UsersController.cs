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

        // GET: api/users
        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _myDbContext.Users.ToList();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id:int}")]
        [Route("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _myDbContext.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/users/byname/{name}
        [HttpGet("byname/{name}")]
        [Route("GetUserByName/{name}")]
        public IActionResult GetUserByName(string name)
        {
            var user = _myDbContext.Users.FirstOrDefault(u => u.Username.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id:int}")]
        [Route("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
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
