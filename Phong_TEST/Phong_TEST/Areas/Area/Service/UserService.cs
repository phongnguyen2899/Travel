using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF.Entiti;
using Phong_TEST.Areas.Area.Models;

namespace Phong_TEST.Areas.Area.Service
{
    public class UserService : IUserService
    {
        dbcontext _context = new dbcontext();
        public bool checkexists(string username)
        {
            var user = _context.UserInfors.FirstOrDefault(x => x.UserName == username);
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public bool Adduser(UserInfor user)
        {
            try
            {
                if (checkexists(user.UserName))
                {
                    return false;
                }
                _context.UserInfors.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Deleteuser(int id)
        {
            var user = _context.UserInfors.FirstOrDefault(x => x.ID == id);
            if (user != null)
            {
                _context.UserInfors.Remove(user);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Edit(UserInfor user)
        {
            throw new NotImplementedException();
        }

        public UserInfor FindUserbyID(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> Getallusers()
        {
            var query = from u in _context.UserInfors
                        join r in _context.Roles
                        on u.RoleID equals r.ID
                        select new UserViewModel
                        {
                            ID = u.ID,
                            UserName = u.UserName,
                            Name = u.Name,
                            PassWord = u.PassWord,
                            Role = r.RoleName
                        };
            return query.ToList();
        }

        public object[] Loginservice(string username, string password)
        {
            var user = from u in _context.UserInfors
                       join r in _context.Roles
                        on u.RoleID equals r.ID
                       where u.UserName == username && u.PassWord == password
                       select new
                       {
                           Role = r.RoleName,
                       };
            if (user.Count() > 0)
            {
                return new object[]
                {
                    user.Count(),
                    user.FirstOrDefault().Role
                };
            }
            return null;
        }
        public List<RoleViewModel> getRole()
        {
            var query = from r in _context.Roles
                        select new RoleViewModel
                        {
                            Rolename = r.RoleName
                        };
            return query.ToList();
        }

        public int PareIdbyRoleName(string role)
        {
            var item = _context.Roles.Where(x => x.RoleName == role).FirstOrDefault();
            if (item == null)
                return -1;

            return item.ID;
        }
    }
}