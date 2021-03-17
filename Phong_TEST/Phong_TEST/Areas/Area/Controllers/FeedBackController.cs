using EF.Entiti;
using PagedList;
using Phong_TEST.Areas.Area.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phong_TEST.Areas.Area.Controllers
{
    [Authorize(Roles = "EDITOR,ADMIN")]
    public class FeedBackController : Controller
    {
        public const int pagesize = 10;
        public IContactService service;
        public FeedBackController()
        {
            service = new ContacService();
        }
        // GET: Area/FeedBack
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAll(int? page)
        {
            IOrderedQueryable<Contact> listcontact = service.Getallcontact();
            //neu bang null return 1
            int pageNumber = (page ?? 1);
            return View(listcontact.ToPagedList(pageNumber, pagesize));
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var check = service.Delete(id);
            if (check == true)
            {
                return Json(new {
                    message = "thanh cong",
                });
            }
            return Json(new
            {
                message = "that bai",
            });
        }
    }
}