using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg_Fishing.Models.Galleries
{
    public class VideoGallery
    {
        public VideoGallery(string name)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.Videos = new HashSet<Video>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Video> Videos { get; private set; }
    }
}
