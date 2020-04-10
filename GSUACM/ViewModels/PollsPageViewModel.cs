using GSUACM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.ViewModels
{
    public class PollsPageViewModel
    {
        public List<Poll> Polls { get; set; }
        public PollsPageViewModel()
        {
            Polls = new List<Poll>();
            SimulatePoll(3, 3);
        }

        private void SimulatePoll(int polls, int options)
        {
            for (int i = 1; i <= polls; i++)
            {
                Polls.Add(new Poll { 
                Title = "Poll "+i,
                Date = DateTime.Now.Date.ToString().Substring(0,9),
                Options = new List<Poll.Option>()
                });
            }
            for (int i = 1; i <= Polls.Count; i++)
            {
                for (int j = 1; j <= options; j++)
                {
                    Polls[i-1].Options.Add(new Poll.Option() { Text = "Poll " + i + " Option " + j });
                }
            }
        }
    }
}
