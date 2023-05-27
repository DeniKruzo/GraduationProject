using GraduationProject.Data.Domains;

namespace GraduationProject.Abstract
{
    public interface IViewComments
    {
        IQueryable<Comment> Comment { get; }

    }
}
