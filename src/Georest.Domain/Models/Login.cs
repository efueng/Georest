using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Domain.Models
{
    public class Login
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Context { get; set; }
        public string Cert { get; set; }
    }
}
