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
        public async Task<IActionResult> RegisterUser(RegisterDTO user){

            var response = await _repo.RegisterUser(_mapper.Map<User>(user), user.Password);

            if(response.Success){

                return Ok(response);
            } 
            return UnprocessableEntity(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserDTO user){

            var response = await _repo.LoginUser(user); 

            if(response.Success){

                return Ok(response);
            }
            return NotFound(response);
        }
    }
}