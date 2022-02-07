using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _204473Q_assignment_2_AS.Models
{
    public class lockoutObj
    {
        public string email { get; set;}
        public DateTime accountExpireDate { get; set; }
        
        public lockoutObj(string Email,DateTime dateExpire)
        {
            email = Email;
            accountExpireDate = dateExpire;
        }
    }
}