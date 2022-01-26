using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MILLIMETER_DESIGN.Models;
using MILLIMETER_DESIGN.Models.Repositories.Interface;
using MILLIMETER_DESIGN.ViweModel.Contactrequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Controllers
{
    [Authorize]
    public class ContactRequestsController : Controller
    {
        private IContactRequestsRepository _contactRequestsRepository;

        public ContactRequestsController(IContactRequestsRepository contactRequestsRepository)
        {
            _contactRequestsRepository = contactRequestsRepository;
        }
        // GET: ImageController
        public IActionResult Index()
        {
            var contactRequests = _contactRequestsRepository.GetRequests();
            var List = new ContactrequestsListViewModel();
            var _contactRequests = new List<ContactrequestsViewModel>();
            foreach (var elem in contactRequests)
            {
                var model = new ContactrequestsViewModel
                {
                    Id = elem.Id,
                    Name = elem.Name,
                    Email = elem.Email,
                    Subject = elem.Subject

                };

                _contactRequests.Add(model);

            }
            List.Contactrequests = _contactRequests;
            return View(List);
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactrequestsViewModel contactrequests)
        {
           
            var _contactrequests = new ContactRequests()
            {
                Name = contactrequests.Name,
                Email = contactrequests.Email,
                Number = contactrequests.Number,
                Subject=contactrequests.Subject
                
            };
            var contactrequestsid = _contactRequestsRepository.Add(_contactrequests);

            return Redirect("/Home/ContactUs");

        }
        public ActionResult Delete(int id)
        {
            var contactrequests = _contactRequestsRepository.GetContactRequests(id);
            _contactRequestsRepository.Delete(contactrequests);
            return RedirectToAction("Index");
        }

        // POST: ImageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var contactrequests = _contactRequestsRepository.GetContactRequests(id);
            _contactRequestsRepository.Delete(contactrequests);
            return RedirectToAction("Index");
        }
    }
}
