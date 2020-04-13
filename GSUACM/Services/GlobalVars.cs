using GSUACM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GSUACM.Services
{
    public static class GlobalVars
    {
        
        public static String fname;
        public static String getFname
        {
            set { fname = value; }
            get { return fname; }
        }
        public static String email;
        public static int userid;
        public static String lname;
        public static String  phone;
        public static String clubpoints;
        public static ObservableCollection<Request> request = new ObservableCollection<Request>();
        
    }

}
