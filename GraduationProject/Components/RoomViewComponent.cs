using GraduationProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GraduationProject.Components
{
    public class RoomViewComponent : ViewComponent
    {
        private readonly GraduationDbContext _context;

        public RoomViewComponent(GraduationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var chats = _context.Chats.ToList();
            return View(chats);
        }
    }
}
