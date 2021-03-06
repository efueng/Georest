﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.ViewModels
{
    public class StudentLabInputViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int StudentId { get; set; }
        public string DateTimeCreated { get; set; }
        public string DateTimePublished { get; set; }
        public bool IsPublished { get; set; }
        public bool IsOverridden { get; set; }
    }
}
