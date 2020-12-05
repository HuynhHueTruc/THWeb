using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Model.Framework;
using PagedList;

namespace Model
{
    public class AccountModel
    {
        private TextThucHanhDbContext context = null;

        public AccountModel()
        {
            context = new TextThucHanhDbContext();
        }
          
        public bool Login(string userName, string Password)
        {
            if(userName != null && Password != null)
            {
                object[] sqlParas =
                           {
               new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", Password),
            };
                var res = context.Database.SqlQuery<bool>("Sp_Account_Login @UserName, @Password", sqlParas).SingleOrDefault();
                return res;
            }else
            {
                return false;
            }
        }
       
        public IEnumerable<Book> ListAllPage(int page, int rowLimit)
        {
            return context.Books.OrderByDescending(x => x.Title).ToPagedList(page, rowLimit);
        }

    }
}