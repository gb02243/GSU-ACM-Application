using GSUACM.Models;
using System;
using System.Collections.Generic;
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
    }

}
