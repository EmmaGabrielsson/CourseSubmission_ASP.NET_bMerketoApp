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
                var contactFormSubmitted = await _contactRepo.GetIdentityAsync(x => x.Name == model.Name && x.Email == model.Email && x.PhoneNumber == model.PhoneNumber && x.Message == model.Message);
                if (contactFormSubmitted == null)
                {
                    await _contactRepo.AddIdentityAsync(model);
                    ModelState.AddModelError ("", "Thank you for contacting us, we get back to you as soon as possible!");
                    return View();
                }
                ModelState.AddModelError("", "You have already sent this contact-form to us");
            }

            return View(model);
        }

    }
}
