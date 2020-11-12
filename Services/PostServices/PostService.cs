using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.PostDTOs;
using BackEnd.Models;

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
        public async Task<ServiceResponse<GetPostDTO>> CreatePost(CreatePostDTO create, int id)
        {
            ServiceResponse<GetPostDTO> response = new ServiceResponse<GetPostDTO>(); 

            try{



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