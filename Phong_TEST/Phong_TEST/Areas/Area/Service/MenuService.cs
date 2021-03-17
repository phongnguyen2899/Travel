using EF.Entiti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Phong_TEST.Areas.Area.Service
{
    public class MenuService : IMenuService
    {
        public dbcontext _context;
        public MenuService()
        {
            _context = new dbcontext();
        }

      

        public bool Create(EF.Entiti.Menu menu)
        {
            try
            {
                _context.Menus.Add(menu);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<EF.Entiti.Menu> GetAllmenu()
        {
            return _context.Menus.ToList();
        }

        public List<EF.Entiti.Menu> Getmenushowhome()
        {
            List<EF.Entiti.Menu> list = _context.Menus.Where(x => x.isActive == true).ToList();
            return list;
        }

        public bool Deletemenu(int id)
        {
            try
            {
                var menu = _context.Menus.Find(id);
                _context.Menus.Remove(menu);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}