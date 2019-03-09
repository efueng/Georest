using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Georest.Domain.Models
{
    public class StudentResponse : Entity
    {
        public int ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }
        public string Body { get; set; }
    }
}
