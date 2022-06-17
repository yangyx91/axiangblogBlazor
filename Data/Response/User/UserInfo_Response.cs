using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class UserInfoDocument
    {
        public int iD { get; set; }
        public string userID { get; set; }
        public string displayName { get; set; }
        public string email { get; set; }
        public string userPrincipalName { get; set; }
        public string password { get; set; }
        public string nickName { get; set; }
        public string headImg { get; set; }
        public string openID { get; set; }
        public string phone { get; set; }
        public DateTime createTime { get; set; }
        public int isDelete { get; set; }
    }
}
