using GraduationProject.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GraduationProject.Components
{
    public class MenuSpecializationViewComponent : ViewComponent
    {
        private readonly IHaveSpecialization specRepo;

        public MenuSpecializationViewComponent(IHaveSpecialization specRepo)
        {
            this.specRepo = specRepo;
        }

        public IViewComponentResult Invoke()
        {
            var list = specRepo.Specialization.Select(s=>s.Name).ToList();

            return View(list);
        }
    }
}
