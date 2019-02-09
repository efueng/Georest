using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Georest.Domain.Models;

namespace Georest.Api.ViewModels
{
    public class LabViewModel
    {
        public Lab Lab { get; set; }
        public string Name { get; set; }
        public string LabID { get; set; }
        public string DateTimeCreated { get; set; }
        public string DateTimePublished { get; set; }
        public bool IsPublished { get; set; }
        public bool IsOverridden { get; set; }
        public LabViewModel(Lab lab)
        {
            Lab = lab;
        }
    }
}
