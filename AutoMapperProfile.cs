using AutoMapper;
using BackEnd.DTOs.PostDTOs;
using BackEnd.DTOs.ThreadDTOs;
using BackEnd.DTOs.UserDTOs;
using BackEnd.Models;

//Profile for Automapper DTOs 
namespace BackEnd
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDTO>();
            CreateMap<AddUserDTO, User>(); 
            CreateMap<RegisterDTO, User>(); 
            CreateMap<Thread, CreateThreadDTO>(); 
            CreateMap<Thread, GetThreadDTO>();
            CreateMap<Posts, GetPostDTO>().ForMember(PostDto => 
                PostDto.User, Post => 
                    Post.MapFrom(p => p.User.UserName)
            );
        }
    }
}