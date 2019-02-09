using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Georest.Domain.Models
{
    public class Lab
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Intro { get; set; }
        public DateTime? DueDate { get; set; }
        public List<Exercise> ExerciseList { get; set; }
    }
}
