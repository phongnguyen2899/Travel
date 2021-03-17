using EF.Entiti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phong_TEST.Areas.Area.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PassWord { get; set; }

        public string Role { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        
    }
}