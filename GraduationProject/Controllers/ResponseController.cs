using GraduationProject.Areas.Identity.Data;
using GraduationProject.Data;
using GraduationProject.Data.Domains;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    public class ResponseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GraduationDbContext context;

        public ResponseController(UserManager<ApplicationUser> userManager, GraduationDbContext context)
        {
             _userManager = userManager;
            this.context = context;
        }

        public IActionResult List(long id)
        {
            IQueryable<Response> data;
         
            data = context.Responses.Where(r => r.RecipientId == _userManager.GetUserId(User));

            if (id != 0)
            {
                data = data.Where(d => d.ProfileOrOrderId == id);
            }


            if (!data.Any())
                return RedirectToAction("About", "Home");

            return View(data);
        }

        public ActionResult Create(int id, string recId)
        {
            var model = new Response()
            {
                RecipientId = recId,
                ProfileOrOrderId = id,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Response model, int id, string recId)
        {
            var newModel = new Response()
            {
                Message = model.Message,
                SenderId = _userManager.GetUserId(User),
                RecipientId = recId,
                ProfileOrOrderId = id
            };

            context.Responses.Add(newModel);

            context.SaveChanges();

            return RedirectToAction("About", "Home");
        }
    }
}
