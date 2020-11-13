using System.Collections.Generic;
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
        /*
            Action: Gets all post in a thread 
            Param: Int post ID 
            Returns: IactionResult with the list of post DTOs 
        */
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosts(int id){

            System.Console.WriteLine(id);
            ServiceResponse<List<GetPostDTO>> response = await _postService.GetPosts(id);
            return response.ReturnStatus();
        }
        /*
            Action: Controller to delete the post 
            Param: GetPostIdDTO - id of the post - claim will give user auth
            Return: IActionResult with bool data 
        */
        [HttpDelete]
        public async Task<IActionResult> DeletePost(GetPostIdDTO postIdDTO){

            ServiceResponse<bool> response = await _postService.DeletePost(postIdDTO, User.GetIdentifier());
            return response.ReturnStatus();
        }
        /*
            Action: Controller Updates the comment of a Post
            Param: post id, comment to update
            Return: IAction result with Service response PostDTO
        */
        [HttpPut]
        public async Task<IActionResult> UpdatePost(UpdatePostDTO getPost){

            ServiceResponse<GetPostDTO> response = await _postService.UpdatePost(getPost, User.GetIdentifier()); 
            return response.ReturnStatus();
        }
    }
}