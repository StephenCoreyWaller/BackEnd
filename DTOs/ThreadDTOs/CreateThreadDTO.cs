//DTO for thread creation 
using BackEnd.Models;

namespace BackEnd.DTOs.ThreadDTOs
{
    public class CreateThreadDTO
    {
        public string Title { get; set; }
        public string Category { get; set; }
    }
}