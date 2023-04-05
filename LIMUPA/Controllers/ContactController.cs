using LIMUPA.Models;
using LIMUPA.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LIMUPA.Controllers
{
    public class ContactController : Controller
    {
        private readonly DataContext _db;

        public ContactController(DataContext db)
        {
            _db = db;
        }
        public IActionResult contactform()
        {
            ContactViewModel model = new ContactViewModel();
            ContactUs contactus = _db.ContactUs.FirstOrDefault();
            model.Contacts = contactus;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult contactform(string customername, string customerEmail, string contactSubject, string contactMessage)
        {
            if (ModelState.IsValid)
            {
                ContactViewModel model = new ContactViewModel();
                ContactForm contactform = new ContactForm
                {
                    Name = customername,
                    Email = customerEmail,
                    Subject = contactSubject,
                    Text = contactMessage
                };
                _db.ContactForms.Add(contactform);
                _db.SaveChanges();
                model.ContactForms = contactform;
                TempData["success"] = "Success";
                return RedirectToAction("contactform");
            }
            else
            {
               TempData["wrong"] = "Xanalar boshdur";
                return RedirectToAction("contactform");
            }
        }
    }
}
