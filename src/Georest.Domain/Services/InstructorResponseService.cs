using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services
{
    public class InstructorResponseService : IInstructorResponseService
    {
        private ApplicationDbContext DbContext { get; }
        public InstructorResponseService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<InstructorResponse> AddResponse(InstructorResponse response)
        {
            DbContext.InstructorResponses.Add(response);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return response;
        }

        public async Task<bool> DeleteResponse(int responseId)
        {
            InstructorResponse fetchedResponse = DbContext.InstructorResponses.Find(responseId);

            if (fetchedResponse != null)
            {
                DbContext.InstructorResponses.Remove(fetchedResponse);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }

            return false;
        }

        public async Task<List<InstructorResponse>> GetAllResponses()
        {
            return await DbContext.InstructorResponses.ToListAsync().ConfigureAwait(false);
        }

        public async Task<InstructorResponse> GetById(int responseId)
        {
            return await DbContext.InstructorResponses.FindAsync(responseId).ConfigureAwait(false);
        }

        public async Task<InstructorResponse> UpdateResponse(InstructorResponse response)
        {
            DbContext.InstructorResponses.Update(response);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return response;
        }
    }
}
