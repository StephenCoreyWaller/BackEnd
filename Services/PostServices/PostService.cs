using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.PostDTOs;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PostService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        /*
            Action: Creates new post
            Params: CreatePostDTO - has thread Id and user Id
            Return: Newly create post entity 
        */
        public async Task<ServiceResponse<GetPostDTO>> CreatePost(CreatePostDTO create, int id)
        {
            ServiceResponse<GetPostDTO> response = new ServiceResponse<GetPostDTO>(); 

            try{
     
                var newPost = await _context.Posts.AddAsync(new Posts{
                    Comment = create.Comment, 
                    DateAndTimeCommented = DateTime.Now,
                    User = await _context.Users.FirstOrDefaultAsync(u => u.Id == id), 
                    Thread = await _context.Threads.FirstOrDefaultAsync(t => t.Id == create.ThreadId)
                });
                await _context.SaveChangesAsync(); 
                response.Data = _mapper.Map<GetPostDTO>(newPost.Entity);

            }catch(Exception ex){

                response.Message = ex.Message; 
                response.Success = false; 
                response.ResultStatusCode = StatusCode.serverError; 
            }
            return response; 
        }
        /*
            Action: Gets all post in a thread
            Param: Int of the thread Id 
            Return: List of post DTOs 
        */
        public async Task<ServiceResponse<List<GetPostDTO>>> GetPosts(int threadId)
        {
            ServiceResponse<List<GetPostDTO>> response = new ServiceResponse<List<GetPostDTO>>(); 

            response.Data = await _context.Posts
                .Include(p => p.User).Where(p => p.Thread.Id == threadId)
                .Select(p => _mapper.Map<GetPostDTO>(p)).ToListAsync();

            if(response.Data == null){

                response.Success = false; 
                response.Message = "No post for the requested thread."; 
                response.ResultStatusCode = StatusCode.NotFound; 
            }
            return response; 
        }
        /*
            Action: Controller to delete the post 
            Param: GetPostIdDTO - id of the post - claim will give user auth
            Return: IActionResult with bool data 
        */
        public async Task<ServiceResponse<bool>> DeletePost(GetPostIdDTO postId, int userId)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>(); 

            try{

                _context.Posts.Remove(await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId.Id && p.User.Id == userId));
                await _context.SaveChangesAsync();
                response.Data = true; 

            }catch(Exception ex){

                response.Data = false; 
                response.Success = false; 
                response.Message = ex.Message; 
            }
            return response; 
        }
        /*
            Action: Updates the comment of a Post
            Param: post id, comment to update, and user Id
            Return: PostDTO
        */
        public async Task<ServiceResponse<GetPostDTO>> UpdatePost(UpdatePostDTO updatePost, int id)
        {
            ServiceResponse<GetPostDTO> response = new ServiceResponse<GetPostDTO>(); 

            try{

                Posts post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == updatePost.Id && p.User.Id == id);
                post.Comment = updatePost.Comment ?? post.Comment; 
                await _context.SaveChangesAsync(); 
                response.Data = _mapper.Map<GetPostDTO>(post);

            }catch(Exception ex){

                response.Message = ex.Message; 
                response.Success = false; 
                response.ResultStatusCode = StatusCode.serverError; 
            }
            return response; 
        }
    }
}