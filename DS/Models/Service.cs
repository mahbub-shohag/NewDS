using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Caption { get; set; }
        public string Detail { get; set; }
        public string DisplayDetail { get; set; }
        public string ImageOriginal { get; set; }
        public string ImageThumb { get; set; }
        public string ImageMid { get; set; }
        public double Cost { get; set; }

    }
}