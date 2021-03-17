using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Phong_TEST.Areas.Area.Service
{
    public interface IMenuService
    {
        List<EF.Entiti.Menu> GetAllmenu();

        bool  Create(EF.Entiti.Menu menu);
        List<EF.Entiti.Menu> Getmenushowhome();
        bool Deletemenu(int id);
    }
}