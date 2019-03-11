using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services
{
    public class InstructorLabService : IInstructorLabService
    {
        private ApplicationDbContext DbContext { get; }
        public InstructorLabService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<InstructorLab> AddLab(InstructorLab lab)
        {
            if (lab == null) throw new ArgumentNullException(nameof(lab));

            DbContext.InstructorLabs.Add(lab);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return lab;
        }

        public async Task<bool> DeleteLab(int labId)
        {
            InstructorLab labToDelete = await DbContext.InstructorLabs.FindAsync(labId).ConfigureAwait(false);

            if (labToDelete == null)
            {
                DbContext.InstructorLabs.Remove(labToDelete);
                DbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public async Task<ICollection<InstructorLab>> GetAllLabs()
        {
            return await DbContext.InstructorLabs.ToListAsync().ConfigureAwait(false);
        }

        public async Task<InstructorLab> GetById(int labId)
        {
            InstructorLab fetchedLab = await DbContext.InstructorLabs.SingleOrDefaultAsync(lab => lab.Id == labId).ConfigureAwait(false);

            return fetchedLab;
        }

        public async Task<ICollection<InstructorLab>> GetLabsForInstructor(int instructorId)
        {
            return (await DbContext.Instructors.FindAsync(instructorId).ConfigureAwait(false)).Labs;

            //return await DbContext.Labs.Where(lab => lab.s)
        }

        public async Task<InstructorLab> UpdateLab(InstructorLab lab)
        {
            if (lab == null) throw new ArgumentNullException(nameof(lab));

            DbContext.InstructorLabs.Update(lab);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return lab;
        }
    }
}