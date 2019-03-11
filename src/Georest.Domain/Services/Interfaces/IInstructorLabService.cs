using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Georest.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Georest.Domain.Services.Interfaces
{
    public interface IInstructorLabService
    {
        Task<InstructorLab> AddLab(InstructorLab lab);
        Task<InstructorLab> GetById(int labId);
        Task<bool> DeleteLab(int labId);
        Task<ICollection<InstructorLab>> GetLabsForInstructor(int instructorId);
        Task<InstructorLab> UpdateLab(InstructorLab lab);
        Task<ICollection<InstructorLab>> GetAllLabs();
    }
}
