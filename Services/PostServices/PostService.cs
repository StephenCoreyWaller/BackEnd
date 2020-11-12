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
        public Task<ServiceResponse<bool>> DeletePost(GetPostIdDTO postId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<List<GetPostDTO>>> GetPosts(int threadId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<GetPostDTO>> UpdatePost(UpdatePostDTO updatePost)
        {
            throw new System.NotImplementedException();
        }
    }
}