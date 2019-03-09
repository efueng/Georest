using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.ViewModels
{
    public class StudentResponseViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        public int ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public ExerciseViewModel Exercise { get; set; }
    }
}
