using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GraduationProject.Data;
using GraduationProject.Data.Domains;
using Microsoft.AspNetCore.Identity;
using GraduationProject.Areas.Identity.Data;
using GraduationProject.Models;
using GraduationProject.Models.ViewModel;

namespace GraduationProject.Controllers
{
    public class CommentsController : Controller
    {
        private readonly GraduationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(GraduationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(long id)
        {
            if(_context.Comment == null) 
                Problem("Entity set 'GraduationDbContext.Comment'  is null.");

            var model = new CommentListViewModel()
            {
                getComments = _context.Comment.Where(c => c.IdProfile == id).ToList(),
                getUsers = _context.Users.ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment updateComment, string id)
        {
            long profId = _context.Profile.First(p=>p.OwnerId == id).ProfileId;
            var model = new Comment()
            {
                IsPositive = updateComment.IsPositive,
                Text = updateComment.Text,
                OwnerId = _userManager.GetUserId(User),
                IdProfile = profId,
                Profiles = _context.Profile.First(p => p.OwnerId == id),
            };
         
            _context.Add(model);

            if(updateComment.IsPositive)
            {
                //добавлять +1 к рейтингу профиля
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("List", "Response");
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CommentId,IsPositive,OwnerId,IdProfile,Text")] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.CommentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }
    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Comment == null)
            {
                return Problem("Entity set 'GraduationDbContext.Comment'  is null.");
            }
            var comment = await _context.Comment.FindAsync(id);
            if (comment != null)
            {
                _context.Comment.Remove(comment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(long id)
        {
          return (_context.Comment?.Any(e => e.CommentId == id)).GetValueOrDefault();
        }
    }
}
