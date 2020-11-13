using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.DTOs.PostDTOs;
using BackEnd.Models;

//Interface for Post CRUD opperations

namespace BackEnd.Services.PostServices
{
    public interface IPostService
    {
        Task<ServiceResponse<GetPostDTO>> CreatePost(CreatePostDTO create, int user);
        Task<ServiceResponse<List<GetPostDTO>>> GetPosts(int threadId);
        Task<ServiceResponse<GetPostDTO>> UpdatePost(UpdatePostDTO updatePost);
        Task<ServiceResponse<bool>> DeletePost(GetPostIdDTO postId, int userId);
    }
}