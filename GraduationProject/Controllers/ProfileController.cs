using GraduationProject.Areas.Identity.Data;
using GraduationProject.BussinesLogic;
using GraduationProject.Data;
using GraduationProject.Data.Domains;
using GraduationProject.Domains;
using GraduationProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GraduationProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly GraduationDbContext _context;
        private IWebHostEnvironment _appEnvironment;

        public SelectList _specialization { get; set; }

        public ProfileController(GraduationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment appEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _specialization = new SelectList(_context.Specialization.Select(n => n.Name).ToList());
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        [Authorize(Roles = "specialist")]
        public IActionResult Add()
        {
            var upModel = new UpdateProfileModel()
            {
                SelectedSpec = _specialization,
            };
            return View(upModel);
        }

        [HttpPost]
        [Authorize(Roles = "specialist")]
        public IActionResult Add(UpdateProfileModel model)
        {
            Profile profile = new Profile
            {
                IsSafeDeal = model.IsSafeDeal,
                AboutMe = model.AboutMe,
                Services = model.Services,
                AvatarImg = ImageMethods.AddFile(_appEnvironment, model.formFile),
                SpecProfile = _context.Specialization.First(c => c.Name == model.GetProfileName),
                OwnerId = _userManager.GetUserId(User),
                IsFree = true,
                Rating = 0,
                LastVisit = DateTime.Now
            };

            _context.Profile.Add(profile);

            _context.SaveChanges();

            return RedirectToAction("OrdersList", "Orders");
        }
    }
}
