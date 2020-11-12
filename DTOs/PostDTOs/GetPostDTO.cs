using System;

namespace BackEnd.DTOs.PostDTOs
{
    public class GetPostDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int NumberOfUpVotes { get; set; }
        public int NumberOfDownVotes { get; set; }
        public DateTime DateAndTimeCommented { get; set; }
        public string User { get; set; }
    }
}