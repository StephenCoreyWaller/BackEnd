using System.Linq;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using BackEnd.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using BackEnd.DTOs.UserDTOs;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;

//MVC controller for CRUD opperations for the USER entitiy
namespace BackEnd.Controllers
{
    [Authorize]
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
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var id = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);

            var user = await _userService.GetUserService(id);

            return user.ReturnStatus();
        } 
        /*
            Delete: Deletes a user from the database
            Parameters: User Id
            Returne: bool of success
        */
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            var id = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var deletedUser = await _userService.DeleteUserService(id);

            return deletedUser.ReturnStatus();
        }
        /*
            Put: Updates a users information 
            Parameters: Id of user 
            Return: Updated user entity 
        */
        [HttpPut]
        public async Task<IActionResult> UpdateUser(AddUserDTO userUpdate){

            var id = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var updatUser = await _userService.UpdateUserService(id, userUpdate);

            return updatUser.ReturnStatus();
        }
    }
}