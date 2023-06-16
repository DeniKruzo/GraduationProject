using GraduationProject.Data;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    public class ChatController : Controller
    {
        private GraduationDbContext _context;

        public ChatController(GraduationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            _context.Chats.Add(new Chat
            {
                Name = name,
                Type = ChatType.Room,
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet("{id:long}")]
        public IActionResult Chat(long id)
        {
            var chat = _context.Chats
                .Include(x => x.Messages)
                .FirstOrDefault(x => x.Id == id);
            return View(chat);
        }
    }
}
