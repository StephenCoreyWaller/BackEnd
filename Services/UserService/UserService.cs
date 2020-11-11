using System;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.UserDTOs;
using BackEnd.Models;
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
                response.ResultStatusCode = StatusCode.NotFound; 
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
                response.ResultStatusCode = StatusCode.serverError; 
            }
            return response;
        }
        /*
            Delete: Deletes a user from the database
            Parameters: User Id
            Returne: bool of success
        */
        public async Task<ServiceResponse<bool>> DeleteUserService(int id)
        {
            var response = new ServiceResponse<bool>(); 
            
            try{

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id); 
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();  

            }catch(Exception ex){

                response.Success = false; 
                response.Message = ex.Message; 
                response.ResultStatusCode = StatusCode.serverError; 
            }
            return response; 
        }
        /*
            Put: Updates a users information 
            Parameters: Id of user 
            Return: Updated user entity 
        */
        public async Task<ServiceResponse<GetUserDTO>> UpdateUserService(int id, AddUserDTO updateUser){

            var response = new ServiceResponse<GetUserDTO>();

            try{
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id); 
                user.AboutMe = updateUser.AboutMe ?? user.AboutMe;
                user.Email = updateUser.Email ?? user.Email; 
                user.FirstName = updateUser.FirstName ?? user.FirstName; 
                user.LastName = updateUser.LastName ?? user.LastName; 
                user.UserName = updateUser.UserName ?? user.UserName; 
                //add password and recruiter change 
                
                await _context.SaveChangesAsync();  
                response.Data = _mapper.Map<GetUserDTO>(user); 
            
            }catch(Exception ex){

                response.Success = false; 
                response.Message = ex.Message; 
                response.ResultStatusCode = StatusCode.serverError; 
            }
            return response; 
        }
    }
}