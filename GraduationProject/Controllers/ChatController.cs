using GraduationProject.Areas.Identity.Data;
using GraduationProject.Data;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace GraduationProject.Controllers
{
    public class ChatController : Controller
    {
        private GraduationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(GraduationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            var chats = _context.Chats
                .Include(x => x.Users)
            .Where(x => !x.Users
                        .Any(x => x.UserId == _userManager.GetUserId(User)))
                .ToList();
            return View(chats);
        }

        public async Task<IActionResult> CreateRoom(string name)
        {
            var model = new Chat
            {
                Name = name,
                Type = ChatType.Room,
            };

            model.Users.Add(new ChatUser
            {
                UserId = _userManager.GetUserId(User).ToString(),
                RoleInChat = UserRoleInChat.Admin
            });

            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Chat");
        }

        public IActionResult Find()
        {
            var users = _context.Users
                .Where(x => x.Id != _userManager.GetUserId(User))
                .ToList();
            return View(users);
        }

        public IActionResult Private()
        {
            var chats = _context.Chats
                .Include(x => x.Users)
                    .ThenInclude(x => x.User)
                .Where(c => c.Type == ChatType.Private && c.Users
                        .Any(y => y.UserId == _userManager.GetUserId(User)))
                .ToList();
            return View(chats);
        }

        public async Task<IActionResult> CreatePrivateRoom(string userId, string namesChat)
        {
            var chat = new Chat
            {
                Name = namesChat,
                Type = ChatType.Private
            };

            var user1 = new ChatUser { UserId = userId };
            var user2 = new ChatUser { UserId = _userManager.GetUserId(User) };

            chat.Users.Add(user1);
            chat.Users.Add(user2);

            _context.Add(chat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Chat", "Chat", new {id=chat.Id});
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
                UserId = _userManager.GetUserId(User),
                RoleInChat = UserRoleInChat.Member
            };

            _context.ChatUsers.Add(chatUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", "Chat", new {id=id});
        }
    }
}
