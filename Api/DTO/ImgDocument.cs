using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DTO
{
    public class ImgDocument
    {
        public string id { get; set; }
        public string rev { get; set; }
        public string imgId { get; set; }
        public string name { get; set; }
        public string upyunPath { get; set; }
        public string url { get; set; }
        public string userId { get; set; }
        public string creator { get; set; }
    }

    public class QueryImgDocumentResult
    {
        public int total_rows { get; set; }

        public int offset { get; set; }

        public List<ImgDocumentKeyValue> rows { get; set; }
    }

    public class ImgDocumentKeyValue
    {
        public string id { get; set; }

        public string key { get; set; }

        public revDoc value { get; set; }

        public ImgDocument doc { get; set; }
    }
}
