using System;
using System.Text;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.DTOs.UserDTOs;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


//Implimintation of IAuthrepository
namespace BackEnd.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _configuration = configuration;
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

            if (await UserExists(user.UserName) || await EmailExist(user.Email)){

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
            if (await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower())){

                return true;
            }
            return false;
        }
        /*
            Action: Checks to see if email has been used for users login
            Params: string email 
            Return: Bool if email has been used by other user 
        */
        public async Task<bool> EmailExist(string email)
        {

            if (await _context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower())){

                return true;
            }
            return false;
        }
        /*
            Action: Creates the hash and salt for user registration 
            Params: Reference byte array salt and hash and string password
            Return: Void 
        */
        private void CreateHashPassword(out byte[] hash, out byte[] salt, string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()){

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
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == userRequest.UserName.ToLower());

            //Add Token in this path
            if (user == null || !GetUserPasswordHash(user, userRequest.Password)){

                response.Success = false;
                response.Message = "User is not found or password is incorrect.";
                return response;
            }
            response.Data = CreateJWT(user);

            return response;
        }
        /*
            Action: Creates byte array of user hash and salt
            Param: User, user password requesting login
            Return: Byte array
        */
        private bool GetUserPasswordHash(User user, string password)
        {
            byte[] computedHash;

            using (var hmac = new System.Security.Cryptography.HMACSHA512(user.Salt)){

                computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            return CheckPassword(user.Hash, computedHash);
        }
        /* 
            Action: Checks that password is correct for user login 
            Param: User from database and password submited from post request 
            return: bool 
        */
        private bool CheckPassword(byte[] passwordHash, byte[] passwordForLogin)
        {
            return passwordHash.SequenceEqual(passwordForLogin);
        }
        /*
            Action: Creates a JWT for authentication
            Param: User object 
            return: string JWT
        */
        private string CreateJWT(User user)
        {
            List<Claim> claim = new List<Claim>{

                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(

                _configuration.GetSection("Appsettings:Token").Value
            ));
            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha384Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor{

                Subject = new ClaimsIdentity(claim), 
                Expires = DateTime.Now.AddDays(1), 
                SigningCredentials = cred
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler(); 
            SecurityToken token = handler.CreateToken(tokenDescriptor); 

            return handler.WriteToken(token);
        }
    }
}