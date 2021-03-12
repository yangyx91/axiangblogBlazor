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

    public class QueryBingImgDocumentResult
    {
        public int affectedDocs { get; set; }

        public List<BingImgDocument> data { get; set; }
    }
}
