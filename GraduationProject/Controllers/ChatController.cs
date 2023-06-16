using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    public class ChatController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
