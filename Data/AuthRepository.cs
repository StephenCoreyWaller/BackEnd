using System.Linq;
using System.Threading.Tasks;
using BackEnd.DTOs.UserDTOs;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

//Implimintation of IAuthrepository
namespace BackEnd.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        /*
            Action: This function will registar a user 
            Params: User model class and string password
            Return: ID of the user 
        */
        public async Task<ServiceResponse<int>> RegisterUser(User user, string password)
        {
            var response = new ServiceResponse<int>(); 

            if(await UserExists(user.UserName) || await EmailExist(user.Email)){

                response.Success = false; 
                response.Message = "User name or email is already taken.";
                return response;
            }

            CreateHashPassword(out byte[] hash, out byte[] salt, password);

            user.Hash = hash; 
            user.Salt = salt; 

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            response.Data = user.Id;
            return response; 
        }
        /*
            Action: Checks to see if username is already in the database
            Params: String user name 
            Return: Bool if user name is in the database 
        */
        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower())){
                
                return true; 
            }
            return false; 
        }
        /*
            Action: Checks to see if email has been used for users login
            Params: string email 
            Return: Bool if email has been used by other user 
        */
        public async Task<bool> EmailExist(string email){

            if(await _context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower())){

                return true; 
            }
            return false; 
        }
        /*
            Action: Creates the hash and salt for user registration 
            Params: Reference byte array salt and hash and string password
            Return: Void 
        */
        private void CreateHashPassword(out byte[] hash, out byte[] salt, string password){

            using(var hmac = new System.Security.Cryptography.HMACSHA512()){

                salt = hmac.Key; 
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        /*
            Action: Checks user name and password for login 
            Params: LoginUserDTO
            return: Login token 
        */
        public async Task<ServiceResponse<string>> LoginUser(LoginUserDTO userRequest)
        {
            var response = new ServiceResponse<string>(); 

            //check for user first return if no user found with user name or password is wrong 

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == userRequest.UserName.ToLower()); 



            return response; 
        }
        /*
            Action: Creates byte array of user hash and salt
            Param: User 
            Return: Byte array
        */
        // private byte[] GetUserPasswordHash(User user){

        //     using(var hmac = new System.Security.Cryptography.HMACSHA512()){

        //         var arr = hmac.
        //     }
        //     return 
        // }
        /* 
            Action: Checks that password is correct for user login 
            Param: User from database and password submited from post request 
            return: bool 
        */
        private bool CheckPassword(byte[] passwordHash, byte[] password){

            return passwordHash.SequenceEqual(password); 
        }    
    }
}