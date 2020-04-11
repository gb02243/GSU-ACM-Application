using GSUACM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.ViewModels
{
    class EventsPageViewModel
    {
        public List<Event> Events { get; private set; }
        public EventsPageViewModel()
        {
            Events = new List<Event>();
            Events.Add(new Event()
            {
                Title = "ACM Meeting",
                Location = "IT Building 2212",
                PostDate = DateTime.Now.ToString(),
                Body = "Come join us for our meeting!",
                EventDate = DateTime.Now.Date.ToString()
            });
            Events.Add(new Event()
            {
                Title = "ACM Resume Workshop",
                Location = "IT Building 2212",
                PostDate = DateTime.Now.ToString(),
                Body = "Come join us for our resume workshop!",
                EventDate = DateTime.Now.AddDays(20).ToString()
            });
            Events.Add(new Event()
            {
                Title = "ACM Guest Speaker",
                Location = "IT Building 2212",
                PostDate = DateTime.Now.ToString(),
                Body = "Come join us to hear from our guest speaker!",
                EventDate = DateTime.Now.AddDays(30).ToString()
            });
        }
    }
}
