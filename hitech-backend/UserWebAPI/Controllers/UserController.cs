using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserWebAPI.DAL;
using UserWebAPI.DTO.Request;
using UserWebAPI.Entities;
using UserWebAPI.Services;

namespace UserWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] UserRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var createdUser = await _userService.CreateUserAsync(request);

            return Ok(createdUser);
        }

        // PUT: api/user/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, [FromBody] UserRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var userToUpdate = await _userService.GetUserByIdAsync(id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            var user = await _userService.UpdateUserAsync(id, request);

            return Ok(user);
        }

        // DELETE: api/user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userToDelete = await _userService.GetUserByIdAsync(id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(id);

            return Ok();
        }
    }
}
