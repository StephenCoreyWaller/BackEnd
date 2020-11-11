using System;
using System.Collections.Generic;
using System.Linq;
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

            if(threadDTO.Title == null && threadDTO.Category == null){

                response.Message = "Category and title are requiered"; 
                response.Success = false; 
                response.ResultStatusCode = StatusCode.BadRequest;
                return response;  
            }
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
                response.ResultStatusCode = StatusCode.serverError;   
            }
            return response; 
        }
        /* 
            Action: Gets all threads for a specific category
            Param: string of the category name 
            Return: List of threads with the same categroy
        */
        public async Task<ServiceResponse<List<GetThreadDTO>>> GetAllTheThreads(string category){

            ServiceResponse<List<GetThreadDTO>> response = new ServiceResponse<List<GetThreadDTO>>(); 
            List<GetThreadDTO> threads = _mapper.Map<List<GetThreadDTO>>(await _context.Threads.Where(t => t.Category == category).ToListAsync());
            response.Data = threads; 

            if(threads.Count == 0){

                response.Message = "0 reuslts returned."; 
            } 
            return response; 
        }
        /*
            Action: Deletes a thread from the database
            Params: thread Id and User Id 
            Return: true if removed from the database
        */
        public async Task<ServiceResponse<bool>> DeleteThread(int threadid, int userId)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();   

            try{

                response.Data = null != _context.Threads.Remove(await _context.Threads.FirstOrDefaultAsync(t => t.Id == threadid && t.User.Id == userId)); 
                await _context.SaveChangesAsync();

            }catch(Exception ex){

                response.Data = false; 
                response.Message = ex.Message; 
                response.Success = false; 
                response.ResultStatusCode = StatusCode.serverError; 
            }
            return response;  
        }
        /*
            Action: Gets all threads by a specific user
            Params: User Id 
            Return: List of thread DTOs of all of the user has posted 
        */
        public async Task<ServiceResponse<List<GetThreadDTO>>> GetThreadsOfUser(int id)
        {
            ServiceResponse<List<GetThreadDTO>> repsonse = new ServiceResponse<List<GetThreadDTO>>(); 

            try{

                repsonse.Data = _mapper.Map<List<GetThreadDTO>>(await  _context.Threads.Where(t => t.User.Id == id).ToListAsync()); 

            }catch(Exception ex){

                repsonse.Message = ex.Message; 
                repsonse.Success = false;
                repsonse.ResultStatusCode = StatusCode.serverError;  
            }
            return repsonse; 
        }
        /*
            Action: Updates the title and category
            Param: updateDTO and user ID 
            Return: Service response with getthreadDTO data 
        */
        public async Task<ServiceResponse<GetThreadDTO>> UpdateThread(UpdateThreadDTO update, int userId)
        {
            ServiceResponse<GetThreadDTO> response = new ServiceResponse<GetThreadDTO>(); 

            try{

                Thread thread = await _context.Threads.FirstOrDefaultAsync(t => t.Id == update.Id && t.User.Id == userId);
                thread.Category = update.Category ?? thread.Category; 
                thread.Title = update.Title ?? thread.Title; 
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetThreadDTO>(thread); 

            }catch(Exception ex){

                response.Success = false; 
                response.Message = ex.Message; 
                response.ResultStatusCode = StatusCode.serverError; 
            }
            return response; 
        }
    }
}