using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.ViewModels
{
    public class StudentResponseInputViewModel
    {
        [Required]
        public string Body { get; set; }
        public int ExerciseId { get; set; }
        public ExerciseViewModel Exercise { get; set; }
    }
}
