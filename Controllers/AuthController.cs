using System;
/*
    Action: Controller for authorization for registraion and login for users. This
    controller uses the Authorizaion services file located in the data directory. 
*/
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.UserDTOs;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; 

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        
        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(AddUserDTO user){

            var response = await _repo.RegisterUser(_mapper.Map<User>(user), user.Password);
 
            return response.ReturnStatus();
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserDTO user){

            string key = "TestKey"; 
            var response = await _repo.LoginUser(user); 
            CookieOptions cookieOptions = new CookieOptions(); 
            cookieOptions.Expires = DateTime.Now.AddHours(3); 
            Response.Cookies.Append(key, response.Data, cookieOptions); 

            return response.ReturnStatus();
        }
    }
}