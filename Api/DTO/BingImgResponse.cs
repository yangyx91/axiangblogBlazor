using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DTO
{
    public class BingImgResponse
    {
        public List<BingImg> images { get; set; }

        //public object tooltips { get; set; }
    }

    public class BingImg
    {
        public string startdate { get; set; }
        public string url { get; set; }
        public string urlbase { get; set; }
        public string copyright { get; set; }
    }
}
