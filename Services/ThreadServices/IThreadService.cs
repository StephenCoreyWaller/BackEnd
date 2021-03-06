//Interface for the thread CRUD opperations 
using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.DTOs.ThreadDTOs;
using BackEnd.Models;

namespace BackEnd.Services.ThreadServices
{
    public interface IThreadService
    {
        Task<ServiceResponse<GetThreadDTO>> CreateThread(CreateThreadDTO threadDTO, int id);
        Task<ServiceResponse<List<GetThreadDTO>>> GetThreadsOfUser(int id);
        Task<ServiceResponse<GetThreadDTO>> UpdateThread(UpdateThreadDTO update, int userId); 
        Task<ServiceResponse<bool>> DeleteThread(int threadId, int userId);
        Task<ServiceResponse<List<GetThreadDTO>>> GetAllTheThreads(); 
    } 
}