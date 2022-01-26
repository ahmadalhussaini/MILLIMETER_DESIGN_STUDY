using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Models.Repositories.Interface
{
  public interface IContactRequestsRepository
    {
        List<ContactRequests> GetRequests();
        ContactRequests Add(ContactRequests contactrequests);
        ContactRequests GetContactRequests(int Id);
        void Delete(ContactRequests contactRequests);


    }
}
