using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.ViewModels
{
    public class ExerciseInputViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string  Body { get; set; }
    }
}
