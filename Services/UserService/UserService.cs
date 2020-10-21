using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.DTOs.UserDTOs;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

//Implementation of the User services 
namespace BackEnd.Services.UserService
{
    public class UserService : IUserService
    {
        public List<User> UserList = new List<User>{
            new User { UserName = "stephen", Id = 1},
            new User { UserName = "Corey", Id = 2},
            new User { UserName = "Jason", Id = 3},
            new User { UserName = "Mom", Id = 4},
            new User { UserName = "brian", Id = 5}
        };
        private readonly IMapper _mapper;
        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }
        /*
            Get Request: Returns the User calls DTO
            Parameters: User Id
            Returns: User information
        */
        public async Task<ServiceResponse<GetUserDTO>> GetUserService(int id)
        {
            var response = new ServiceResponse<GetUserDTO>();
            response.Data = _mapper.Map<GetUserDTO>(UserList.FirstOrDefault(u => u.Id == id));

            if (response.Data == null)
            {
                response.Success = false;
                response.Message = "No User Found";
            }
            return response;
        }
        /*
            Post Request: Creates new user for the database
            Parameters: UserName, Hash, Is a reqruiter 
            Returns: Status of request  
        */
        public async Task<ServiceResponse<GetUserDTO>> CreateUserService(AddUserDTO newUser)
        {
            var response = new ServiceResponse<GetUserDTO>();

            try{
                //Get the return after implimenting the database 
                UserList.Add(_mapper.Map<User>(newUser));

            }catch(Exception ex){

                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}