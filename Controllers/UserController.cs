using System.Collections.Generic;
using System.Linq;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

//MVC controller for CRUD opperations for the USER entitiy
namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public List<User> UserTestList = new List<User>{
            new User { Name = "stephen", Id = 1}, 
            new User { Name = "Corey", Id = 2}, 
            new User { Name = "Jason", Id = 3}, 
            new User { Name = "Mom", Id = 4}, 
            new User { Name = "brian", Id = 5}
        }; 

        //Returns the User calls DTO 
        [HttpGet("{id}")]
        public IActionResult GetUser(int id){

            var user = UserTestList.FirstOrDefault(u => u.Id == id); 

            if(user != null){
                
                return Ok(user); 
            }
            return NotFound(); 
        }
    }
}