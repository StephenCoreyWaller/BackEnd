//Gets thread DTO 
using System;
using System.Collections.Generic;
using BackEnd.Models;

namespace BackEnd.DTOs.ThreadDTOs
{
    public class GetThreadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime DateAndTimeCreated { get; set; }
        public string User { get; set; }

    }
}