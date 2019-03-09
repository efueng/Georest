using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Georest.Domain.Models;

namespace Georest.Api.ViewModels
{
    public class InstructorLabViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimePublished { get; set; }
        public ICollection<ExerciseViewModel> Exercises { get; set; }
        public ICollection<InstructorResponse> Responses { get; set; }

        // The instructor is the author of the lab
        public InstructorViewModel Instructor { get; set; }
        public int InstructorId { get; set; }
    }
}
