using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Georest.Domain.Models
{
    public class Exercise : Entity
    {
        [Required]
        public string Title
        {
            get => _Title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title is required for Exercise.", nameof(value));
                }

                value = value.Trim();
                _Title = value;
            }
        }
        private string _Title;

        [Required]
        public string Body
        {
            get => _Body;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Body is required for Exercise.", nameof(value));
                }

                value = value.Trim();
                _Body = value;
            }
        }
        private string _Body;
    }
}