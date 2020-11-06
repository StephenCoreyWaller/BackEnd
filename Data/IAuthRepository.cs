using System.Threading.Tasks;
using BackEnd.DTOs.UserDTOs;
using BackEnd.Models;

//Interface for user repository model - Register, Login and check if user exists 
namespace BackEnd.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> RegisterUser(User user, string password);
        Task<ServiceResponse<string>> LoginUser(LoginUserDTO user);
        Task<bool> UserExists(string username);
    }
}