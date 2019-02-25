using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Domain.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Response { get; set; }
        public List<Exercise> ExerciseList { get; set; }
    }
}
