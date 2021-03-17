using EF.Entiti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phong_TEST.Areas.Area.Service
{
    public interface IContactService
    {
        bool Create(Contact contact);
        IOrderedQueryable<Contact> Getallcontact();
        bool Delete(int id);
    }
}