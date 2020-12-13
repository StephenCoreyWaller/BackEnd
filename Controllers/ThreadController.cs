using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackEnd.DTOs.ThreadDTOs;
using BackEnd.Models;
using BackEnd.Services.ThreadServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
/*
    Controller for CRUD operations for threads.  
*/

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
                threadDTO, User.GetIdentifier());
            return response.ReturnStatus();
        }
        /*
            Action: Returns all threads in a category 
            Param: category id
            return: all threads in action result 
            development note: create a category entity to seperate threads 
        */
        [AllowAnonymous]
        [HttpGet()]
        public async Task<IActionResult> GetAllThreads(){ 
        
            ServiceResponse<List<GetThreadDTO>> response = await _threadService.GetAllTheThreads(); 
            return response.ReturnStatus();
        }
        /*
            Action: Deletes thread 
            Param: Thread ID
            return: Iaction 
            Note: fix database to remove the relation to the posts when deleted. keeping the post 
        */
        [HttpDelete]
        public async Task<IActionResult> DeleteThread(ThredIdDTO thread){

            ServiceResponse<bool> response = await _threadService.DeleteThread(
                thread.Id, User.GetIdentifier()); 
            return response.ReturnStatus();
        }
        /*
            Action: Gets all the users threads that were statrted 
            Param: None - claims will determine the threads returned 
            Return: List of users threads 
        */
        [HttpGet("usersthreads")]
        public async Task<IActionResult> GetUserThreads(){

            ServiceResponse<List<GetThreadDTO>> response = await _threadService.GetThreadsOfUser(User.GetIdentifier()); 
            return response.ReturnStatus();  
        } 
        /*
            Actio: Updates the category or title of a thread 
            Param: UpdateDTO
            Retruns: ActionResult 
        */
        [HttpPut]
        public async Task<IActionResult> UpdateThread(UpdateThreadDTO update){

            ServiceResponse<GetThreadDTO> response = await _threadService.UpdateThread(update, User.GetIdentifier());
            return response.ReturnStatus(); 
        }
    }
}