using System; 
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.ThreadDTOs;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

//Implimitation for the thread services

namespace BackEnd.Services.ThreadServices
{
    public class ThreadService : IThreadService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ThreadService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        /*
            Action: Creates thread 
            Params: CreateThreadDTO and the users Id from the contorller 
            Return: response with the created thread DTO
        */
        public async Task<ServiceResponse<GetThreadDTO>> CreateThread(CreateThreadDTO threadDTO, int id)
        {
            ServiceResponse<GetThreadDTO> response = new ServiceResponse<GetThreadDTO>();

            try{
                Thread thread = new Thread {
                    Title = threadDTO.Title, 
                    Category = threadDTO.Category, 
                    DateAndTimeCreated = DateTime.Now, 
                    User = await _context.Users.FirstOrDefaultAsync(u => u.Id == id) 
                };
                response.Data = _mapper.Map<GetThreadDTO>((await _context.Threads.AddAsync(thread)).Entity);
                response.Data.UserName = (await _context.Users.FirstOrDefaultAsync(u => u.Id == id)).UserName; 
                await _context.SaveChangesAsync();

            }catch (Exception ex){

                response.Message = ex.Message;
                response.Success = false; 
            }
            return response; 
        }
        public Task<ServiceResponse<bool>> DeleteThread(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<GetThreadDTO>> GetThread(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<GetThreadDTO>> UpdateThread(UpdateThreadDTO update)
        {
            throw new System.NotImplementedException();
        }
    }
}