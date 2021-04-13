using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class UploadFile_Response
    {
        public int affectedDocs { get; set; }

        public List<WxUploadDocument> data { get; set; }
    }


    public class WxUploadDocument
    {
        public string _id { get; set; }
        public string FileID { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }

        public long FileSize { get; set; }

        public string UploadDate { get; set; }
    }

    public class QueryWxUploadDocumentParams
    {
        public int page { get; set; }

        public int pageSize { get; set; }
    }

    public class DelWxUploadDocumentParams
    {
        public string imgUrl { get; set; }
    }
}
