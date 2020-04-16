using GSUACM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GSUACM.ViewModels
{
    public class PollsPageViewModel
    {
        public List<Poll> Polls { get; set; }
        public List<Poll> ActivePolls { get; set; }
        public List<Poll> PastPolls { get; set; }
        public PollsPageViewModel()
        {
            Polls = new List<Poll>();
            ActivePolls = new List<Poll>();
            PastPolls = new List<Poll>();
            SimulatePoll(6, 3);
            CategorizePolls();
        }

        private void CategorizePolls()
        {
            foreach (Poll poll in Polls)
            {
                if (poll.isActive)
                    ActivePolls.Add(poll);
                else
                    PastPolls.Add(poll);
            }
        }

        private void SimulatePoll(int polls, int options)
        {
            for (int i = 1; i <= polls; i++)
            {
                Polls.Add(new Poll { 
                Title = "Poll "+i,
                Date = DateTime.Now.Date.ToString().Substring(0,9),
                Options = new ObservableCollection<Poll.Option>()
                });
            }
            for (int i = 1; i <= Polls.Count; i++)
            {
                for (int j = 1; j <= options; j++)
                {
                    Polls[i-1].Options.Add(new Poll.Option() { Text = "Poll " + i + " Option " + j });
                }
            }
            for (int i = 1; i <= polls; i++)
            {
                if(i > 3)
                {
                    Polls[i-1].isActive = true;
                }
            }
        }
    }
}
