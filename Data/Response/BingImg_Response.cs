using System;
using System.Collections.Generic;

namespace Data
{
    public class BingImg_Response 
    {
        public int affectedDocs { get; set; }

        public List<BingImgDocument> data { get; set; }
    }

    public class BingImg
    {
        public string imgId { get; set; }
        public string CreateDate { get; set; }
        public string Url { get; set; }
        public string UrlBase { get; set; }
        public string Title { get; set; }
    }


    public class BingImgDocument
    {
        public string _id { get; set; }
        public string imgId { get; set; }
        public string CreateDate { get; set; }
        public string Url { get; set; }
        public string UrlBase { get; set; } 
        public string Title { get; set; }
        public string Creator { get; set; }
    }

    public class QueryBingImgParams
    {
        public int page { get; set; }

        public int pageSize { get; set; }
    }
}
