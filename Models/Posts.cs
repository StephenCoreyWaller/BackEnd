//Class model for posts of thread 
using System;

namespace BackEnd.Models
{
    public class Posts
    {
        public int Id { get; set; }
        public Thread ThreadId { get; set; }
        public string Comment { get; set; }
        public User UserId { get; set; }
        public int NumberOfUpVotes { get; set; }
        public int NumberOfDownVotes { get; set; }
        public DateTime DateAndTimeCommented { get; set; }
    }
}