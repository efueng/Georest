using Georest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services.Interfaces
{
    public interface IInstructorResponseService
    {
        Task<InstructorResponse> AddResponse(InstructorResponse response);
        Task<InstructorResponse> GetById(int responseId);
        Task<bool> DeleteResponse(int responseId);
        Task<ICollection<InstructorResponse>> GetAllResponses();
        Task<InstructorResponse> UpdateResponse(InstructorResponse response);
    }
}
