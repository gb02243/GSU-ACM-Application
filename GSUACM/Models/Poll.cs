using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Models
{
    public class Poll
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public List<Option> Options { get; set; }
        public int TotalVotes { get; set; }
        public bool isActive { get; set; }
        public class Option
        {
            public string Text { get; set; }
            public int Votes { get; set; }
        }
    }
}
