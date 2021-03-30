using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class WxPostTypes_Response
    {
        public int affectedDocs { get; set; }

        public List<WxPostTypeDocument> data { get; set; }
    }


    public class WxPostTypeDocument
    {
        public string PostTypeId { get; set; }

        public string PostType { get; set; }

        public int PostTypeLevel { get; set; }

        public string PostTypeParentId { get; set; }

        public string Author { get; set; }

        public string PostTypeDate { get; set; }
    }
}
