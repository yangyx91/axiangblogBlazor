using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class QueuePostDocument
    {
        public string post_Id { get; set; }

        public DateTime post_Date { get; set; }

        public string post_Title { get; set; }

        public string post_Content { get; set; }

        public string post_CategoryName { get; set; }

        public string post_Status { get; set; }

        public string post_Author { get; set; }
    }
}
