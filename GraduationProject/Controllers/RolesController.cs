using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GraduationProject.Controllers
{
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index() => View(_roleManager.Roles.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }
    }
}
