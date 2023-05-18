using Microsoft.AspNetCore.Mvc;
using WebbApp.Models.Entities;
using WebbApp.Models.ViewModels;
using WebbApp.Repositories;

namespace WebbApp.Controllers
{
    public class ContactsController : Controller
    {
        #region Constructors & Private Fields

        private readonly ContactFormRepo _contactRepo;

        public ContactsController(ContactFormRepo contactRepo)
        {
            _contactRepo = contactRepo;
        }
        #endregion
        public IActionResult Index()
        {
            ViewData["Title"] = "Contact Us";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormViewModel model)
        {
            ViewData["Title"] = "Contact Us";

            if (ModelState.IsValid)
            {
                ContactFormEntity contactForm = new()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Message = model.Message,
                    CompanyName = model.Company,
                    PhoneNumber = model.PhoneNumber,
                };

                var contactFormSubmitted = await _contactRepo.GetIdentityAsync(x => x.Name == contactForm.Name && x.Email == contactForm.Email && x.Message == contactForm.Message && x.CompanyName == contactForm.CompanyName && x.PhoneNumber == contactForm.PhoneNumber);
                if (contactFormSubmitted == null)
                {
                    await _contactRepo.AddIdentityAsync(contactForm);
                    ModelState.AddModelError ("", "Thank you for contacting us, we get back to you as soon as possible!");
                    return View(model);
                }
                ModelState.AddModelError("", "You have already sent this contact-form to us");
            }

            return View(model);
        }

    }
}
