using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Models
{
    public class Request
    {

        public string subject{get;set;}
        public string Date { get; set; }
        public int userid { get; set; }
        public string username { get; set; }
        public string tutorname { get; set; }
        public int sessionID { get; set; }
    }
}
