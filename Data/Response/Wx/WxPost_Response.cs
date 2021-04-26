using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class WxPost_Response
    {
        public int affectedDocs { get; set; }

        public List<WxPostDocument> data { get; set; }
    }

    public class WxPostDocument
	{
        public string PostId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string PostUrl { get; set; }
		public string Author { get; set; }
        public DateTime PostDateTime { get; set; }
        public string PostDate { get; set; }
		public string Tags { get; set; }
		public string Thumbnail { get; set; }
		public bool IsTop { get; set; }
		public string PostType { get; set; }
		public string Topic { get; set; }

	}

    public class QueryWxPostParams
    {
        public int page { get; set; }

        public int pageSize { get; set; }

        public string postId { get; set; }

        public string postType { get; set; }

        public string title { get; set; }
    }

    public class DelWxPostParams
    {
        public string postId { get; set; }
    }
}
