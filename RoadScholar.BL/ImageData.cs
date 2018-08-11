using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class ImageData : ActivityData
    {
        public string Title { get; set; }
        public string URL { get; set; }

        public ImageData()
        {
            this.Type = "Image";
        }

        public ImageData(string Title, string URL, string ActivityName) : base(ActivityName)
        {
            this.Title = Title;
            this.URL = URL;
            this.Type = "Image";
        }

        public ImageData(ImageData image) : base(image)
        {
            this.Title = image.Title;
            this.URL = image.URL;
            this.Type = "Image";
        }
    }
}
