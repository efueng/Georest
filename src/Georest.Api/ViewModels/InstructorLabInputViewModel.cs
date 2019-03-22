using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.ViewModels
{
    public class InstructorLabInputViewModel
    {
        [Required]
        public string Title { get; set; }
        public string DateTimeCreated { get; set; }
        public string DateTimePublished { get; set; }
        public bool IsPublished { get; set; }
        public bool IsOverridden { get; set; }
        [Required]
        public int InstructorId { get; set; }
        public ICollection<int> ExerciseIds { get; set; }

    }
}
