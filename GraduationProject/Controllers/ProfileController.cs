using GraduationProject.Abstract;
using GraduationProject.Areas.Identity.Data;
using GraduationProject.BussinesLogic;
using GraduationProject.Data;
using GraduationProject.Data.Domains;
using GraduationProject.Domains;
using GraduationProject.Models;
using GraduationProject.Models.ViewModel;
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
        private readonly IGetProfiles _getProfiles;
        private readonly IHaveSpecialization _getSpec;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GraduationDbContext _context;
        private IWebHostEnvironment _appEnvironment;

        public SelectList _specialization { get; set; }

        public ProfileController(GraduationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment appEnvironment, IGetProfiles getProfiles, IHaveSpecialization getSpec)
        {
            _userManager = userManager;
            _context = context;
            _specialization = new SelectList(_context.Specialization.Select(n => n.Name).ToList());
            _appEnvironment = appEnvironment;
            _getProfiles = getProfiles;
            _getSpec = getSpec;
        }

        [HttpGet]
        [Authorize(Roles = "specialist")]
        public IActionResult Add()
        {
            string thisId = _userManager.GetUserId(User);
            if (!_context.Profile.Any(p => p.OwnerId == thisId))
            {
                var upModel = new UpdateProfileModel()
                {
                    SelectedSpec = _specialization,
                };
                return View(upModel);
            }
            else
            {
                return RedirectToAction("Bridge", new { id = thisId });
            }
            
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

        [HttpGet]
        [Route("Profile/List")]
        [Route("Profile/List/{specilization}")]
        public IActionResult List(string specilization)
        {
            string _specilization = specilization;
            IQueryable<Profile> profile = null;
            string currSpec = "";

            if (string.IsNullOrEmpty(specilization))
            {
                profile = _getProfiles.Profile;
            }
            else
            {
                if (string.Equals(specilization.ToString(), specilization, StringComparison.OrdinalIgnoreCase))
                {
                    profile = _getProfiles
                        .Profile
                        .Where(i => i.SpecProfile.Name.Equals(specilization.ToString()));
                }

                currSpec = _specilization;
            }
            var model = new ProfileListViewModel
            {
                getAllProfiles = profile,
                specialization = currSpec,
                getUsers = _context.Users.ToList()
            };
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "specialist")]
        public IActionResult Edit(string id) 
        {
            var profileEdit = _context.Profile
                .FirstOrDefault(i => i.OwnerId == id);

            var model = new UpdateProfileModel(profileEdit)
            {
                SelectedSpec = _specialization,
            };

            return View(model);
        }

        [Authorize(Roles = "specialist")]
        [HttpPost]
        public IActionResult Edit(UpdateProfileModel model)
        {
            model.AvatarImg = ImageMethods.AddFile(_appEnvironment, model.formFile);
            model.SpecProfile = _context.Specialization.First(c => c.Name == model.GetProfileName);
            model.OwnerId = _userManager.GetUserId(User);
            model.IsFree = true;
            model.LastVisit = DateTime.Now;

            _context.Profile.Update(model);

            _context.SaveChanges();

            return RedirectToAction("OrdersList", "Orders");
        }

        public IActionResult Bridge(string id)
        {
            var profile = _context.Profile
               .FirstOrDefault(i => i.OwnerId == id);

            return View(profile);
        }
    }
}
