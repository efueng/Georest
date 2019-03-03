using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Georest.Web.Models
{
    public class User
    {
        public int EWUid { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
        public string Group { get; set; }
        //public ViewModels.LabViewModel CurrentLabState { get; set; }
    }
}