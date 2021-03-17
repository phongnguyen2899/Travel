using Microsoft.Owin.Security;
using Phong_TEST.Areas.Area.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using EF.Entiti;
using System.Web.Security;
using Phong_TEST.Areas.Area.Models;

namespace Phong_TEST.Areas.Area.Controllers
{
 
    
    public class UserController : Controller
    {
        
        public IUserService service;
        public UserController()
        {
            service = new UserService();
        }
        // GET: Area/User
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Getalluser");
            }
            return View();
        }
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("GetAll", "Feedback");
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }
        public void setCookie(string username, bool rememberme = false, string role = "normal")
        {
            var authTicket = new FormsAuthenticationTicket(
                               1,
                               username,
                               DateTime.Now,
                               DateTime.Now.AddMinutes(120),
                               rememberme,
                               role
                               );

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);
        }

        [HttpPost]
        public ActionResult Login(string username,string password, string ReturnUrl)
        {
            object[] obj = service.Loginservice(username,password);

            if (ModelState.IsValid)
            {
                if (obj != null)
                {
                    setCookie(username, false,obj[1].ToString());
                    return RedirectToAction("GetAll","Feedback");
                }
            }
            ViewBag.Error = "Wrong username or password!";
            return View();
        }


        
        [HttpGet]
        [Authorize(Roles="ADMIN")]
        public ActionResult Getalluser()
        {
            if (Response.StatusCode == 500)
            {
                return View("err403");
            }
            List<UserViewModel> listuser = service.Getallusers();
            return View(listuser);
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public JsonResult Deleteuser(int id)
        {
            if (Response.StatusCode == 500)
            {
            }
            dbcontext db = new dbcontext();
            var user = db.UserInfors.FirstOrDefault(x => x.ID == id);
            bool result = service.Deleteuser(id);
            if (result != false)
            {
                return Json(
                    new
                    {
                        ma = user.ID,
                        mess = "thanh cong"
                    }
                    );
            }
            return Json(
                new
                {
                    ma = 0,
                    mess = "that bai"
                }
                );
        }
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            if (Response.StatusCode == 500)
            {
                return View("err403");
            }
            List<RoleViewModel> listrole = service.getRole();
            UserViewModel model = new UserViewModel();
            model.Roles = listrole;
            ViewBag.listRole = model;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create(UserInfor user,string role) 
        {
            if (Response.StatusCode == 500)
            {
                return View("err403");
            }
            int id = service.PareIdbyRoleName(role);
            user.RoleID = id;
            service.Adduser(user);
            return RedirectToAction("Getalluser");
        }
    }
}