//Class model for the starting thread 
using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public class Thread
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime DateAndTimeCreated { get; set; }
        public User User { get; set; }
        public List<Posts> Posts { get; set; }
    }
}