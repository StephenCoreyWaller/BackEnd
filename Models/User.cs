//Class model for the User entity  

namespace BackEnd.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salt { get; set; }
        public int Hash { get; set; }
        public string Email { get; set; }
        public string AboutMe { get; set; }
        public bool Recruiter { get; set; }
    }
}