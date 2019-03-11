using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services
{
    public class InstructorService : IInstructorService
    {
        private ApplicationDbContext DbContext { get; }
        public InstructorService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<Instructor> AddInstructor(Instructor instructor)
        {
            DbContext.Instructors.Add(instructor);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return instructor;
        }

        public async Task<bool> DeleteInstructor(int instructorId)
        {
            Instructor fetchedInstructor = DbContext.Instructors.Find(instructorId);

            if (fetchedInstructor != null)
            {
                DbContext.Instructors.Remove(fetchedInstructor);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }

            return false;
        }

        public async Task<ICollection<Instructor>> GetAllInstructors()
        {
            return await DbContext.Instructors.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Instructor> GetById(int instructorId)
        {
            return await DbContext.Instructors.FindAsync(instructorId).ConfigureAwait(false);
        }

        public async Task<Instructor> UpdateInstructor(Instructor instructor)
        {
            DbContext.Instructors.Update(instructor);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return instructor;
        }
    }
}
