using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services
{
    public class ExerciseService : IExerciseService
    {
        private ApplicationDbContext DbContext { get; }
        public ExerciseService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<Exercise> AddExercise(Exercise exercise)
        {
            DbContext.Exercises.Add(exercise);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return exercise;
        }

        public async Task<bool> DeleteExercise(int exerciseId)
        {
            Exercise fetchedExercise = DbContext.Exercises.Find(exerciseId);

            if (fetchedExercise != null)
            {
                DbContext.Exercises.Remove(fetchedExercise);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }

            return false;
        }

        public async Task<ICollection<Exercise>> GetAllExercises()
        {
            return await DbContext.Exercises.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Exercise> GetById(int exerciseId)
        {
            return await DbContext.Exercises.FindAsync(exerciseId).ConfigureAwait(false);
        }

        public async Task<Exercise> UpdateExercise(Exercise exercise)
        {
            DbContext.Exercises.Update(exercise);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return exercise;
        }
    }
}
