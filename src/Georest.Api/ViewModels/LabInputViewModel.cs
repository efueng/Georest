using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.ViewModels
{
    public class LabInputViewModel
    {
        public string Name { get; set; }
        public string LabID { get; set; }
        public string DateTimeCreated { get; set; }
        public string DateTimePublished { get; set; }
        public bool IsPublished { get; set; }
        public bool IsOverridden { get; set; }
    }
}
