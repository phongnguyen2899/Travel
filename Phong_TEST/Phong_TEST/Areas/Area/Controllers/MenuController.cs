using EF.Entiti;
using Phong_TEST.Areas.Area.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phong_TEST.Areas.Area.Controllers
{
    [Authorize(Roles = "EDITOR,ADMIN")]
    public class MenuController : Controller
    {
        public IMenuService service;
        public MenuController()
        {
            service = new MenuService();
        }
        // GET: Area/Menu
        public ActionResult Index()
        {
            List<EF.Entiti.Menu> listmenu = service.GetAllmenu();
            return View(listmenu);
        }
        [HttpGet]
        public ActionResult Create()
        {
            dbcontext db = new dbcontext();
            var query = from m in db.Menus
                        select new
                        {
                            id = m.ID
                        };
            ViewBag.idre = query.ToString();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            bool result = service.Create(menu);
            if (!result)
                return View();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult UpdateMenu(int id,int active)
        {
            dbcontext db = new dbcontext();
            var menuitem = db.Menus.Find(id);
            if (active == 1)
            {
                menuitem.isActive = true;
            }
            else
            {
                menuitem.isActive = false;
            }
            
            db.SaveChanges();
            return Json(
                    new
                    {
                        ma=menuitem.ID,
                        mess="thanh cong"
                    }
                );

        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool check = service.Deletemenu(id);
            if (check == true)
            {
                return Json(
                    new {
                        mess= "success"
                    }
                  );
            }
            return Json(
                    new
                    {
                        mess = "err"
                    }
                  );
        }

         
    }
}