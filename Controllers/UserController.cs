using System.Threading.Tasks;
using BackEnd.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using BackEnd.DTOs.UserDTOs;

//MVC controller for CRUD opperations for the USER entitiy
namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /*
            Get: Returns the User calls DTO
            Parameters: User Id
            Return: User information GetUserDTO
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserService(id);

            if (user != null)
            {
                return Ok(user);
            }
            return NotFound(user);
        }
        /*
            Post: Creates new user for the database
            Parameters: Name, Hash, Is a reqruiter 
            Return: User GetUserDTO
        */
        [HttpPost]
        public async Task<IActionResult> CreateUser(AddUserDTO newUser)
        {
            var userCreated = await _userService.CreateUserService(newUser);

            if (userCreated.Success == true)
            {
                return Ok(userCreated);
            }
            return BadRequest(userCreated);
        }
    }
}