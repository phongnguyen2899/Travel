using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF.Entiti;

namespace Phong_TEST.Areas.Area.Service
{
    public class ArticleService : IArticleService
    {
        public dbcontext _context;
        public ArticleService()
        {
            _context = new dbcontext();
        }
        public bool Createarticle(Article article)
        {
            try
            {
                _context.Articles.Add(article);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Deletearticle(int id)
        {
            var article = _context.Articles.Where(x => x.ID == id).FirstOrDefault();
            if (article != null)
            {
                _context.Articles.Remove(article);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Article> Getallarticle()
        {
            return _context.Articles.ToList();
        }

        public List<Menu> Getnamemenu()
        {
            return _context.Menus.Where(x=>x.Level>1).ToList();
        }

        public IOrderedQueryable<Article> SortbyId()
        {
            return (from a in _context.Articles select a).OrderBy(x => x.ID);
        }
        public int ParenIDMenu(string namemenu)
        {
            var menu = _context.Menus.Where(x => x.NameMenu == namemenu).FirstOrDefault();
            if (menu != null)
                return menu.ID;
            return -1;

        }

        public Article FindArticlebyId(int id)
        {
            return _context.Articles.Find(id);
        }

        public bool EditArticle(Article article)
        {
            Article oldartchicle = _context.Articles.Find(article.ID);
            if (oldartchicle != null)
            {
                
                try
                {
                    _context.Articles.Remove(oldartchicle);
                    _context.Articles.Add(article);
                    _context.SaveChanges();
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public List<Menu> getcategory()
        {
            return _context.Menus.Where(x => x.Level > 1).ToList();
        }

        public List<Article> Getrecent(int id)
        {
            var archicle = _context.Articles.Where(x => x.ID == id).FirstOrDefault();

            return _context.Articles.Where(x => x.MenuID == archicle.MenuID).ToList();
        }
    }
}