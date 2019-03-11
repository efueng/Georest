﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Georest.Domain.Models
{
    public class Student : Entity
    {
        public int SectionId { get; set; }
        [ForeignKey("SectionId")]
        public Section Section { get; set; }

        [Required]
        public string FirstName
        {
            get => _FirstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("First name is required for student.", nameof(value));
                }

                value = value.Trim();
                _FirstName = value;
            }
        }
        private string _FirstName;
        
        [Required]
        public string LastName
        {
            get => _LastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Last name is required for student.", nameof(value));
                }

                value = value.Trim();
                _LastName = value;
            }
        }
        private string _LastName;

        public List<StudentLab> Labs { get; set; }
    }
}
