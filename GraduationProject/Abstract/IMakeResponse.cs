using GraduationProject.Data.Domains;

namespace GraduationProject.Abstract
{
    public interface IMakeResponse
    {
        IQueryable<Response> Responses { get; }
    }
}
