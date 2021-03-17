using EF.Entiti;
using Phong_TEST.Areas.Area.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phong_TEST.Areas.Area.Service
{
    public interface IUserService
    {
        object[] Loginservice(string username, string password);
        List<UserViewModel> Getallusers();
        bool Deleteuser(int id);
        bool Adduser(UserInfor user);
        UserInfor FindUserbyID(int id);
        bool Edit(UserInfor user);
        List<RoleViewModel> getRole();
        int PareIdbyRoleName(string role);
    }
}