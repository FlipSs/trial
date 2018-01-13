using System.Web.Mvc;
using Xm.Trial.Models;

namespace Xm.Trial.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(string msg)
        {
            return View(new ErrorViewModel { Message = msg });
        }
    }
}