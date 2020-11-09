using System.Linq;
using System.Security.Claims;
/*
    Controller for CRUD operations for threads.  
*/
using System.Threading.Tasks;
using BackEnd.DTOs.ThreadDTOs;
using BackEnd.Models;
using BackEnd.Services.ThreadServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class ThreadController : ControllerBase
    {
        private readonly IThreadService _threadService;
        public ThreadController(IThreadService threadService)
        {
           _threadService = threadService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateThread(CreateThreadDTO threadDTO)
        {
            ServiceResponse<GetThreadDTO> response = new ServiceResponse<GetThreadDTO>(); 

            if(threadDTO.Title == null && threadDTO.Category == null){

                response.Message = "Category and title are requiered"; 
                response.Success = false; 
                return BadRequest(response); 
            }

            response = await _threadService.CreateThread(
                threadDTO, int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value));

            return Ok(response);
        }
    }
}