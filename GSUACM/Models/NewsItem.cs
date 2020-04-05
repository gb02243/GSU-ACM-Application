using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Models
{
    public class NewsItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        // TODO: test with a string shorter than 120
        public string BodyPreview => Body.Substring(0, 120)+"...";
    }
}
