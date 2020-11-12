using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.PostDTOs;
using BackEnd.Models;
using BackEnd.Services.PostServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(DataContext context, IPostService postService)
        {
            _postService = postService;
        }
        /*  
            Action: Creats a post
            Param: CreateDTO passing in a string comment 
            Return: ActionResult
        */
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDTO create)
        {
            ServiceResponse<GetPostDTO> response = await _postService.CreatePost(create, User.GetIdentifier());
            return response.ReturnStatus();
        }
    }
}