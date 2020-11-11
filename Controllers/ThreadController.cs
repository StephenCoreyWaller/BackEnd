using System.Collections.Generic;
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
        /*
            Action: Controller for creating thread 
            Param: Body of http request create thread dto
            return: action result 
        */
        [HttpPost]
        public async Task<IActionResult> CreateThread(CreateThreadDTO threadDTO)
        {   
            ServiceResponse<GetThreadDTO> response = await _threadService.CreateThread(
                threadDTO, int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value));

            return response.ReturnStatus();
        }
        /*
            Action: Returns all threads in a category 
            Param: category id
            return: all threads in action result 
            development note: create a category entity to seperate threads 
        */
        [AllowAnonymous]
        [HttpGet("{category}")]
        public async Task<IActionResult> GetAllThreads(string category){ 
        
            ServiceResponse<List<GetThreadDTO>> response = await _threadService.GetAllTheThreads(category); 

            return response.ReturnStatus();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteThread(ThredIdDTO thread){

            ServiceResponse<bool> response = await _threadService.DeleteThread(
                thread.Id, int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value)); 

            return response.ReturnStatus();
        }
        [HttpGet]
        public async Task<IActionResult> GetUserThreads(){

            ServiceResponse<List<GetThreadDTO>> response = await _threadService.GetThreadsOfUser(GetIdentifier()); 

            return response.ReturnStatus();  
        }
        private int GetIdentifier(){

            return int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value); 
        }
    }
}