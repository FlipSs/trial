using System;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.IO;
using Xm.Trial.Models.Data;
using Xm.Trial.Models;
using System.Linq;
using System.Collections.Generic;
using Xm.Trial.Services;

namespace Xm.Trial.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly DataContext _context;
        private readonly IMailSender _mailSender;
        private readonly IAppConfiguration _appConfiguration;

        public ContactUsController(DataContext context,
            IMailSender mailSender,
            IAppConfiguration appConfiguration)
        {
            _context = context;
            _mailSender = mailSender;
            _appConfiguration = appConfiguration;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Index(ContactUsForm feedbackMsg, IEnumerable<HttpPostedFileBase> screenshots)
        {
            feedbackMsg.SentDate = DateTimeOffset.UtcNow;

            ValidateModel(feedbackMsg);

            if (ModelState.IsValid)
            {
                _context.ContactUsForms.Add(feedbackMsg);
                await _context.SaveChangesAsync();

                var attachmentsPaths = new List<string>();

                if (screenshots.Any())
                {
                    string directoryName = Server.MapPath(_appConfiguration.ContactUsData.FilesFolder + feedbackMsg.ID + "/");
                    Directory.CreateDirectory(directoryName);
                    feedbackMsg.ScreenshotsPath = directoryName;

                    foreach (var file in screenshots)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        file.SaveAs(directoryName + fileName);

                        attachmentsPaths.Add(directoryName + fileName);
                    }
                }

                string msgBody = _appConfiguration.ContactUsData.MsgBody.Replace("{Name}", feedbackMsg.Name)
                    .Replace("{Message}", feedbackMsg.Message)
                    .Replace("{Email}", feedbackMsg.Email);

                await _mailSender.SendEmailAsync(_appConfiguration.ContactUsData.MsgSubject,
                    msgBody, true, attachmentsPaths);

                    await _context.SaveChangesAsync();
                    return View("Sent", new ContactUsViewModel { Name = feedbackMsg.Name });
            }

            return View();
        }
    }
}