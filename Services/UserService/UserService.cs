using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.UserDTOs;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Implementation of the User services 
namespace BackEnd.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public UserService(IMapper mapper, DataContext context)
        {
            _context = context;
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
            response.Data = _mapper.Map<GetUserDTO>(await _context.Users.FirstOrDefaultAsync(u => u.Id == id));

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

            try
            {
                var user = await _context.Users.AddAsync(_mapper.Map<User>(newUser));
                await _context.SaveChangesAsync(); 
                response.Data = _mapper.Map<GetUserDTO>(user.Entity); 
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}