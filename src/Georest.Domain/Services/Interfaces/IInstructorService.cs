using Georest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services.Interfaces
{
    public interface IInstructorService
    {
        Task<Instructor> AddInstructor(Instructor instructor);
        Task<Instructor> GetById(int studentId);
        Task<bool> DeleteInstructor(int studentId);
        Task<List<Instructor>> GetAllInstructors();
        Task<Instructor> UpdateInstructor(Instructor student);
    }
}
