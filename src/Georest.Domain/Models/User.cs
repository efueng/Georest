using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
        public string Group { get; set; }
        //public ViewModels.LabViewModel CurrentLabState { get; set; }
    }
}
