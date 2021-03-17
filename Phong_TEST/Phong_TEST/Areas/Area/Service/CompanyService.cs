using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF.Entiti;

namespace Phong_TEST.Areas.Area.Service
{
    public class CompanyService : ICompanyService
    {
        public dbcontext _context = new dbcontext();
        public CompanyService()
        {
            _context = new dbcontext();

        }
        public bool Create(Company company)
        {
            try
            {
                _context.Companies.Add(company);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Editcompany(Company ct)
        {
            try
            {
                Company company = FindbyID(ct.ID);
                company.MetaDecriptions = ct.MetaDecriptions;
                company.NameCompany = ct.NameCompany;
                company.Tel = ct.Tel;
                company.MetaTitle = ct.MetaTitle;
                company.HotLine = ct.HotLine;
                company.Email = ct.Email;
                company.Address = ct.Address;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public Company FindbyID(int id)
        {
            return _context.Companies.Find(id);
        }

        public List<Company> Getallcompany()
        {
            return _context.Companies.ToList();
        }
    }
}