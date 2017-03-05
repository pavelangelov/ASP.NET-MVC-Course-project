using Bg_Fishing.Utils.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg_Fishing.Models.Galleries
{
    public class Video
    {
        public Video(string title, string url, DateTime postedon)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Title = title;
            this.Url = url;
            this.PostedOn = postedon;
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
