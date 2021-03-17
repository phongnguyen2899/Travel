using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF.Entiti;

namespace Phong_TEST.Areas.Area.Service
{
    public class ContacService : IContactService
    {
        public dbcontext _context;
        public ContacService()
        {
            _context = new dbcontext();
        }
        public bool Create(Contact contact)
        {
            try
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var contact = _context.Contacts.Find(id);
            try
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IOrderedQueryable<Contact> Getallcontact()
        {
            return _context.Contacts.OrderBy(x=>x.ID);
        }
    }
}