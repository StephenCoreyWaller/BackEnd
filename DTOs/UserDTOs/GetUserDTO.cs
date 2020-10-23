namespace BackEnd.DTOs.UserDTOs
{
    public class GetUserDTO 
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AboutMe { get; set; }
        public bool Recruiter { get; set; }
    }
}