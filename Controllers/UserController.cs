using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

//MVC controller for CRUD opperations for the USER entitiy
namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public User userTest = new User{ Name = "stephen"}; 

        //Returns the User calls DTO 
        [HttpGet]
        public IActionResult GetUser(){
            
            return Ok(userTest); 
        }
    }
}