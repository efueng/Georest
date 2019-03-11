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
        public List<ExerciseInputViewModel> Exercises { get; set; }
        public List<InstructorResponseInputViewModel> Responses { get; set; }
    }
}
