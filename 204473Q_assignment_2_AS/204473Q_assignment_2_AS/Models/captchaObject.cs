using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _204473Q_assignment_2_AS.Models
{
    public class captchaObject
    {
        public string success { get; set; }
        public List<string> errorMsg { get; set; }
        public string challenge_ts { get; set; }
        public string hostname { get; set; }
        public string score { get; set; } 
        public string action { get; set; }
    }
}