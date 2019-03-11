using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services
{
    public class StudentResponseService : IStudentResponseService
    {
        private ApplicationDbContext DbContext { get; }
        public StudentResponseService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<StudentResponse> AddResponse(StudentResponse response)
        {
            DbContext.StudentResponses.Add(response);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return response;
        }

        public async Task<bool> DeleteResponse(int responseId)
        {
            StudentResponse fetchedResponse = DbContext.StudentResponses.Find(responseId);

            if (fetchedResponse != null)
            {
                DbContext.StudentResponses.Remove(fetchedResponse);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }

            return false;
        }

        public async Task<ICollection<StudentResponse>> GetAllResponses()
        {
            return await DbContext.StudentResponses.ToListAsync().ConfigureAwait(false);
        }

        public async Task<StudentResponse> GetById(int responseId)
        {
            return await DbContext.StudentResponses.FindAsync(responseId).ConfigureAwait(false);
        }

        public async Task<StudentResponse> UpdateResponse(StudentResponse response)
        {
            DbContext.StudentResponses.Update(response);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return response;
        }
    }
}
