using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GSUACM.Models
{
    public class Poll
    {
        public string PollAuthorID { get; set; }
        public string PollID { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public ObservableCollection<Option> Options { get; set; }
        public int TotalVotes { get; set; }
        public bool isActive { get; set; }
        public class Option
        {
            public string PollID { get; set; }
            public string OptionID { get; set; }
            public string Text { get; set; }
            public int Votes { get; set; }
            public string OptionPlaceHolder { get; set; }
        }
    }
}
