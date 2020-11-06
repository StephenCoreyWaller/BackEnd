//Class model for the User entity  

using System.Collections.Generic;

namespace BackEnd.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }
        public string Email { get; set; }
        public string AboutMe { get; set; }
        public bool Recruiter { get; set; }
        public List<Thread> Threads { get; set; }
        public List<Posts> Posts { get; set; }
    }
}