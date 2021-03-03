using System;
using System.Collections.Generic;

namespace Data
{
    public class BingImg_Response 
    {
        public List<BingImg> images { get; set; }

        //public object tooltips { get; set; }
    }

    public class BingImg
    {
        public string imgId { get; set; }
        public string createDate { get; set; }
        public string url { get; set; }
        public string urlbase { get; set; }
        public string copyright { get; set; }
        public string title { get; set; }
    }


    public class BingImgDocument
    {
        public string id { get; set; }
        public string rev { get; set; }
        public string imgId { get; set; }
        public string CreateDate { get; set; }
        public string Url { get; set; }
        public string UrlBase { get; set; } 
        public string Title { get; set; }
        public string Creator { get; set; }
    }

    public class QueryBingImgDocumentResult
    {
        public int total_rows { get; set; }

        public int offset { get; set; }

        public List<BingImgDocumentKeyValue> rows { get; set; }
    }

    public class BingImgDocumentKeyValue
    {
        public string id { get; set; }

        public string key { get; set; }

        public revDoc value { get; set; }

        public BingImgDocument doc { get; set; }
    }
}
