using EF.Entiti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phong_TEST.Areas.Area.Service
{
    public interface IArticleService
    {
        List<Article> Getallarticle();
        IOrderedQueryable<Article> SortbyId();
        bool Createarticle(Article article);
        bool Deletearticle(int id);
        bool EditArticle(Article article);
        List<Menu> Getnamemenu();
        int ParenIDMenu(string namemenu);
        Article FindArticlebyId(int id);
        List<Menu> getcategory();
        List<Article> Getrecent(int id);
    }
}