using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DTO
{
    public class ImgDocument
    {
        public string id { get; set; }
        public string rev { get; set; }
        public string ImgId { get; set; }
        public string Name { get; set; }
        public string UpyunPath { get; set; }
        public string Url { get; set; }
        public string UserId { get; set; }
        public string Creator { get; set; }
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
