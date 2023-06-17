using GraduationProject.Data;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

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
            var chats = _context.Chats
                .Include(x => x.Users)
                .Where(x => !x.Users
                        .Any(x => x.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
                .ToList();
            return View(chats);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room,
            };

            chat.Users.Add(new ChatUser
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                RoleInChat = UserRoleInChat.Admin
            });
            _context.Chats.Add(chat);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Chat");
        }

        [HttpGet("{id:long}")]
        public IActionResult Chat(long id)
        {
            var chat = _context.Chats
                .Include(x => x.Messages)
                .FirstOrDefault(x => x.Id == id);
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(long ChatId, string message)
        {
         
            var Message = new Message
            {
                ChatId = ChatId,
                Text = message,
                Name = User.Identity.Name,
                TimeStamp = DateTime.Now
            };

            _context.Messages.Add(Message);
            await _context.SaveChangesAsync();
            return RedirectToAction("Chat","Chat",new {id=ChatId});
        }

        [HttpGet]
        public async Task<IActionResult> JoinRoom(long id)
        {
            var chatUser = new ChatUser
            {
                ChatId = id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                RoleInChat = UserRoleInChat.Member
            };

            _context.ChatUsers.Add(chatUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", "Chat", new {id=id});
        }
    }
}
