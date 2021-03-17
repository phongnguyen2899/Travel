using EF.Entiti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phong_TEST.Areas.Area.Service
{
    public class UserManager
    {
        public bool IsValid(string username, string password)
        {
            dbcontext db = new dbcontext();
                // for the sake of simplicity I use plain text passwords
                // in real world hashing and salting techniques must be implemented   
                return db.UserInfors.Any(u => u.UserName == username
                    && u.PassWord == password);
        }
    }
}