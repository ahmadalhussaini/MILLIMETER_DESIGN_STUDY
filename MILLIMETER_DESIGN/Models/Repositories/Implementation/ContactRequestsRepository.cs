using Microsoft.EntityFrameworkCore;
using MILLIMETER_DESIGN.Models.Context;
using MILLIMETER_DESIGN.Models.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Models.Repositories.Implementation
{
    public class ContactRequestsRepository : IContactRequestsRepository
    {
        MILLIMETER_DESIGNContext _db;

        public ContactRequestsRepository(MILLIMETER_DESIGNContext db)
        {
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _db = db;

        }
        public ContactRequests Add(ContactRequests entity)
        {
            var contactrequests = _db.ContactRequestss.Add(entity);
            _db.SaveChanges();
            return contactrequests.Entity;
        }

        public void Delete(ContactRequests entity)
        {
            _db.ContactRequestss.Remove(entity);
            _db.SaveChanges();
        }

        public ContactRequests GetContactRequests(int Id)
        {
            var contactRequests = _db.ContactRequestss.SingleOrDefault(b => b.Id == Id);
            return contactRequests;
        }

        public List<ContactRequests> GetRequests()
        {
            var contactrequests = _db.ContactRequestss.ToList();
            return contactrequests;
        }
       
    }
}
