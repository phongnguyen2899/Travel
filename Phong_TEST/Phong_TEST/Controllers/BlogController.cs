using EF.Entiti;
using PagedList;
using Phong_TEST.Areas.Area.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phong_TEST.Controllers
{
    public class BlogController : Controller
    {
        const int pagesize = 8;
        public IArticleService service;
        // GET: Blog
        public BlogController()
        {
            service = new ArticleService();
        }
        public ActionResult Index(int page=0)
        {
            /*
            IOrderedQueryable<Article> list = service.SortbyId();
            //neu bang null return 1
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pagesize));
            */
            dbcontext db = new dbcontext();
            var count = db.Articles.Count();
            var data = db.Articles.OrderBy(x => x.ID);
            var model = data.Skip(page * pagesize).Take(pagesize).ToList();
            ViewBag.Maxpage = (count / pagesize) - (count % pagesize == 0 ? 1 : 0);
            ViewBag.page = page;
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            Article article = service.FindArticlebyId(id);
            Session["idarticle"] = id;
            return View(article);
        }
        public ActionResult Getcategory()
        {
            var listcategory = service.getcategory();
            return View(listcategory);
        }
        public ActionResult GetRecentArticle()
        {
            int a = (int)Session["idarticle"];
            if (Session["idarticle"] != null)
            {
                List<Article> articles = service.Getrecent((int)Session["idarticle"]).OrderBy(x=>x.ID).Take(2).ToList();

                if (articles.Count < 2)
                {
                    List<Article> preventive = service.Getallarticle().OrderBy(x => x.ID).Take(2).ToList();
                    return View(preventive);

                }
                return View(articles);
            }
            return View();
        }
        public ActionResult Bloghome()
        {
            List<Article> articles = service.Getallarticle().OrderBy(x => x.ID).Take(4).ToList();
            return View(articles);
        }

        public ActionResult Category(int id)
        {
            /*
            IOrderedQueryable<Article> list = service.SortbyId();
            //neu bang null return 1
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pagesize));
            */
            dbcontext db = new dbcontext();
            var model = db.Articles.Where(x => x.MenuID == id).ToList();
            return View(model);
        }
    }
}