using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment1;
using Assignment1.Models;
using Umbraco.Web.Mvc;
using System.Net.Mail;

namespace Assignment1.Controllers
{
    public class ContactSurfaceController : SurfaceController
    {

    public ActionResult RenderForm()
        {
            return PartialView("_Contact");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                SendEmail(model);
                return RedirectToCurrentUmbracoPage();
            }

            return CurrentUmbracoPage();
        }

        private void SendEmail(ContactModel model)
        {
            MailMessage message = new MailMessage(model.EmailAddress, "william.ros98@gmail.com");
            message.Subject = string.Format("Enquiry from {0} {1} - {2}", model.FirstName, model.LastName,
                model.EmailAddress);
            message.Body = model.Message;
            SmtpClient client = new SmtpClient("127.0.0.1", 25);
            client.Send(message);
        }
    }
}