using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geolabs_Core.Models
{
    public class Login //whatever SAML needs
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Context { get; set; }
        public string Cert { get; set; }
    }
}