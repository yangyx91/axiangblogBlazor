using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DTO
{
    public class BingImgModel
    {
        public ObjectId _id { get; set; }

        public string imgId { get; set; }
        public string CreateDate { get; set; }
        public string Url { get; set; }
        public string UrlBase { get; set; }
        public string Domain { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
    }

    public class CountOneBingImgCondition
    {
        public string imgId { get; set; }
    }
}
