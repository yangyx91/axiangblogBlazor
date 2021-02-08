using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Response
{
    public class DescribeImg_Response
    {
        public DescribeImg description { get;set;}

        public string requestId { get; set; }

        public DescribeImgMetaData metadata { get; set; }
    }

    public class DescribeImg
    {
        public List<string> tags { get; set; }

        public List<textCaption> captions { get; set; }
    }

    public class textCaption
    {
        public string text { get; set; }

        public decimal confidence { get; set; }
    }

    public class DescribeImgMetaData
    {
        public int height { get; set; }

        public int width { get; set; }

        public string format { get; set; }
    }
}
