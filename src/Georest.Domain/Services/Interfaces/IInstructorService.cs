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
        Task<Instructor> GetById(int intstructorId);
        Task<bool> DeleteInstructor(int intstructorId);
        Task<ICollection<Instructor>> GetAllInstructors();
        Task<Instructor> UpdateInstructor(Instructor intstructor);
    }
}
