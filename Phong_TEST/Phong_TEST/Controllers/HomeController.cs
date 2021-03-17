using CaptchaMvc.HtmlHelpers;
using Common;
using EF.Entiti;
using Phong_TEST.Areas.Area.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phong_TEST.Controllers
{
    public class HomeController : Controller
    {
        public IMenuService service;
        public IContactService contact;
        public HomeController()
        {
            service = new MenuService();
            contact = new ContacService();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TopMenu()
        {
            /*
            List<String> list = new List<string>() {"home","contact" };
            ViewBag.list = list;
            return View();*/
            List<EF.Entiti.Menu> list = service.Getmenushowhome();
            return View(list);
        }
        public ActionResult Getlogo()
        {
            dbcontext db = new dbcontext();
            var logo = db.Companies.FirstOrDefault();
            return View(logo);
        }

        [HttpPost]
        public ActionResult CreateContact(string email,string name,string message)
        {

            if (!this.IsCaptchaValid(""))
            {
                ViewBag.error = "Invalid Captcha";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.account = "";
                Mailhelper mail = new Mailhelper();
                mail.SendMail(email, Const.Contac, Const.notification);
                contact.Create(new Contact
                {
                    Name = name,
                    Email = email,
                    Request = message,
                    CreateDate = DateTime.Now
                });
                mail.SendMail(Const.EmailAdmin, "Website ITT", "Khách hàng " + name + " đã gửi phản hồi  Nội dung: " + message + " Ngày gửi:" + DateTime.Now + "");
                return RedirectToAction("Index");
            }
        }
        public ActionResult Getinfo()
        {
            dbcontext db = new dbcontext();
            var info = db.Companies.FirstOrDefault();
            return View(info);
        }
    }
}