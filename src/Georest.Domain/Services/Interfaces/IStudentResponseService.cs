using Georest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services.Interfaces
{
    public interface IStudentResponseService
    {
        Task<StudentResponse> AddResponse(StudentResponse response);
        Task<StudentResponse> GetById(int responseId);
        Task<bool> DeleteResponse(int responseId);
        Task<List<StudentResponse>> GetAllResponses();
        Task<StudentResponse> UpdateResponse(StudentResponse response);
    }
}
