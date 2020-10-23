using System;
using System.Threading.Tasks;
using BackEnd.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using BackEnd.DTOs.UserDTOs;
using BackEnd.Models;

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
        /*
            Delete: Deletes a user from the database
            Parameters: User Id
            Returne: bool of success
        */
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deletedUser = await _userService.DeleteUserService(id);

            if (deletedUser.Success)
            {
                return Ok(deletedUser);
            }
            return BadRequest(deletedUser);
        }
        /*
            Put: Updates a users information 
            Parameters: Id of user 
            Return: Updated user entity 
        */
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, AddUserDTO userUpdate){

            var updatUser = await _userService.UpdateUserService(id, userUpdate);

            if(updatUser.Success){

                return Ok(updatUser); 
            }
            return BadRequest(updatUser);
        }
    }
}