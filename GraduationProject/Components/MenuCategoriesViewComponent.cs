using GraduationProject.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Components
{
    public class MenuCategoriesViewComponent : ViewComponent
    {
        private readonly IOrderCategory _catRepo;

        public MenuCategoriesViewComponent(IOrderCategory catRepo)
        {
            _catRepo = catRepo;
        }

        public IViewComponentResult Invoke()
        {
            var category = _catRepo.AllCategories
                .Select(c => c.Name)
                .ToList();

            return View(category);
        }
    }
}
