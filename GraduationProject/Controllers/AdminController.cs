using GraduationProject.Abstract;
using GraduationProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GraduationProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IOrderCategory _orderCategory;

        public AdminController(IOrderCategory orderCategory)
        {
            _orderCategory = orderCategory;
        }

        public IActionResult CategoryList() => View(_orderCategory.AllCategories.ToList());

        public IActionResult CreateCategory() => View();

        //[HttpPost]
        //public async Task<IActionResult> CreateCategory(string name)
        //{
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        IdentityResult result = await _orderCategory.CreateAsync(new CategoryOrder());
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("CategoryList");
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //        }
        //    }
        //    return View(name);
        //}
    }
}
