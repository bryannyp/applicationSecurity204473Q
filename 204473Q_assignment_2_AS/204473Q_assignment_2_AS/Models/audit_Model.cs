using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _204473Q_assignment_2_AS.Models
{
    public class audit_Model
    {
        public string email{ get; set;}
        public string type { get; set;}

        public DateTime dateandtime { get; set; }
        public audit_Model(string Email, string Type, DateTime Dateandtime)
        {
            email = Email;
            type = Type;
            dateandtime = Dateandtime;
        }
        public enum sessionType{
           Login,
           logout,
           register
        }
    }
}