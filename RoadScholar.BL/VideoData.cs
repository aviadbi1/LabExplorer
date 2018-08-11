using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class VideoData : ActivityData
    {
        public string URL { get; set; }

        public VideoData()
        {
            this.Type = "Video";
        }

        public VideoData(string URL, string ActivityName) : base(ActivityName)
        {
            this.URL = URL;
            this.Type = "Video";
        }

        public VideoData(VideoData video) : base(video)
        {
            this.URL = video.URL;
            this.Type = "Video";
        }
    }
}
