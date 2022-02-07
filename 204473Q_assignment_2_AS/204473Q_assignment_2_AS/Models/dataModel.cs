using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _204473Q_assignment_2_AS.Models
{
    public class dataModel
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string credit_card_info{get;set;}
        public string email { get; set; }
        public string password { get; set; }
        public string date_of_birth { get; set; }
        public byte[] photo { get; set; }
        public string IV { get; set; }
        public string cryptokey { get; set; }
        public string salt { get; set; }
        public dataModel(string firstname, string lastname, string credit, string emailInp, string passwordInp, string date_of_Birth, byte[] photoInp, string iv, string cryptoKey, string Salt)
        {
            first_name = firstname;
            last_name = lastname;
            credit_card_info = credit;
            email = emailInp;
            password = passwordInp;
            date_of_birth = date_of_Birth;
            photo = photoInp;
            IV = iv;
            cryptokey = cryptoKey;
            salt = Salt;
        }
    }
}