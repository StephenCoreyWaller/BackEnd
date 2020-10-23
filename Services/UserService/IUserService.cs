using System.Threading.Tasks;
using BackEnd.DTOs.UserDTOs;
using BackEnd.Models;

//Interface for user service CRUD operations 
namespace BackEnd.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<GetUserDTO>> GetUserService(int id); 
        Task<ServiceResponse<GetUserDTO>> CreateUserService(AddUserDTO newUser);   
        Task<ServiceResponse<bool>> DeleteUserService(int id);     
        Task<ServiceResponse<GetUserDTO>> UpdateUserService(int id, AddUserDTO updateUser); 
    }
}