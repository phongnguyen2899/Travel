using EF.Entiti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phong_TEST.Areas.Area.Service
{
    public interface ICompanyService
    {
        List<Company> Getallcompany();
        bool Create(Company company);
        Company FindbyID(int id);
        bool Editcompany(Company ct);
    }
}