using EF.Entiti;
using PagedList;
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
    public class ArticleController : Controller
    {
        const  int pagesize= 3;

        public IArticleService service;
        public ArticleController()
        {
            service = new ArticleService();
        }
        // GET: Area/Article
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Getall(int? page)
        {
            IOrderedQueryable<Article> list = service.SortbyId();
            //neu bang null return 1
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pagesize));
        }
        public ActionResult Create()
        {
            List<Menu> listmenu = service.Getnamemenu();
            ViewBag.listmenu = listmenu;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Article article,string category, HttpPostedFileBase fileupload)
        {
            int id = service.ParenIDMenu(category);
            if (id == -1)
            {
                return View();
            }
            else
            {
                article.MenuID = id;
                
                if (fileupload != null && fileupload.ContentLength > 0)
                {
                    var filename = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Accsess/Image/Article"), filename);
                    fileupload.SaveAs(path);
                    article.Image = filename;
                }
                service.Createarticle(article);
            }
            return RedirectToAction("Getall");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Article article = service.FindArticlebyId(id);
            ViewBag.menuselected = article.MenuID;
            List<Menu> listmenu = service.Getnamemenu();
            ViewBag.listmenu = listmenu;
            ViewBag.Image = article.Image;
            Session["oldfile"] = article.Image;
            return View(article);
        }
        [HttpPost]
        public ActionResult Edit(Article article, string category, HttpPostedFileBase fileupload)
        {
            //thay đổi idmenu của bài viết mới
            int id = service.ParenIDMenu(category);
            article.MenuID = id;

            //co thay doi file
            if (fileupload != null)
            {
                if (Session["oldfile"] != null)
                {
                    string pathfile = @"C:\Users\Phong\Desktop\IIT\Phong_TEST\Phong_TEST\Accsess\Image\Article\" + Session["oldfile"].ToString()+"";
                    deletefile(pathfile);
                }
                

                var filename = Path.GetFileName(fileupload.FileName);
                var path = Path.Combine(Server.MapPath("~/Accsess/Image/Article"), filename);
                fileupload.SaveAs(path);
                article.Image = filename;
                service.EditArticle(article);
                return RedirectToAction("Getall");
            }
            else
            {
                if (Session["oldfile"] != null)
                {
                    article.Image = Session["oldfile"].ToString();
                }
                
                service.EditArticle(article);
                return RedirectToAction("Getall");
            }

            //khong thay doi file
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

        [HttpGet]
        public ActionResult Details(int id)
        {
            var article = service.FindArticlebyId(id);
            return View(article);
        }
    }
}