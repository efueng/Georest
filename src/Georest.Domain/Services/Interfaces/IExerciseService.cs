using Georest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<Exercise> AddExercise(Exercise exercise);
        Task<Exercise> GetById(int exerciseId);
        Task<bool> DeleteExercise(int exerciseId);
        Task<ICollection<Exercise>> GetAllExercises();
        Task<Exercise> UpdateExercise(Exercise exercise);
    }
}
