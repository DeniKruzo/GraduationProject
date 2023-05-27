using GraduationProject.Abstract;
using GraduationProject.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Data.Repository
{
    public class CommentRepository : IViewComments
    {
        private readonly GraduationDbContext appDbContext;

        public CommentRepository(GraduationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IQueryable<Comment> Comment => appDbContext.Comment.Include(s => s.Profiles);

    }
}
