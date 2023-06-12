using GraduationProject.Areas.Identity.Data;
using GraduationProject.Data.Domains;

namespace GraduationProject.Models.ViewModel
{
    public class CommentListViewModel : Comment
    {
        public IEnumerable<Comment> getComments { get; set; }

        public IEnumerable<ApplicationUser> getUsers { get; set; }
    }
}
