using EF.Entiti;
using Phong_TEST.Areas.Area.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phong_TEST.Areas.Area.Controllers
{
    [Authorize(Roles = "EDITOR,ADMIN")]
    public class CompanyController : Controller
    {
        public ICompanyService service;
        public CompanyController()
        {
            service = new CompanyService();

        }
        // GET: Area/Company
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllCompany()
        {
            List<Company> list = service.Getallcompany();
            if (list.Count >= 1)
            {
                ViewBag.check = false;
                return View(list);
            }
            else
            {
                ViewBag.check = true;
                return View(list);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Company company, HttpPostedFileBase fileupload)
        {
            if (fileupload != null && fileupload.ContentLength > 0)
            {
                var filename = Path.GetFileName(fileupload.FileName);
                var path = Path.Combine(Server.MapPath("~/Accsess/Image/Logo"), filename);
                fileupload.SaveAs(path);
                company.Logo = filename;
                service.Create(company);
            }
            return RedirectToAction("GetAllCompany");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Company company = service.FindbyID(id);
            Session["oldfilecompany"] = company.Logo;
            return View(company);
        }
        [HttpPost]
        public ActionResult Edit(Company ct, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                Company company = service.FindbyID(ct.ID);
                if (fileupload != null)
                {
                    if (Session["oldfilecompany"] != null)
                    {
                        string pathfile = @"C:\Users\Phong\Desktop\IIT\Phong_TEST\Phong_TEST\Accsess\Image\Logo\" + Session["oldfilecompany"].ToString() + "";
                        deletefile(pathfile);
                    }
                    var filename = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Accsess/Image/Logo"), filename);
                    fileupload.SaveAs(path);
                    company.Logo = filename;
                    bool check = service.Editcompany(ct);
                }
            }
            return RedirectToAction("GetAllCompany");
        }


        public bool deletefile(string pathfile)
        {
            if (System.IO.File.Exists(pathfile))
            {
                try
                {
                    System.IO.File.Delete(pathfile);
                    return true;
                }
                catch (System.IO.IOException e)
                {

                }
            }
            return false;
        }


    }
}