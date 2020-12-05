using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TextThucHanh.Areas.Admin.Code
{
    [Serializable] //Tự động hoá nhị phân
    public class UserSession
    {
        private string UserName { set; get; }
        public UserSession(string UserName)
        {
            this.UserName = UserName;
        }

    }
}