using GraduationProject.Data.Domains;

namespace GraduationProject.Abstract
{
    public interface IHaveSpecialization
    {
        IQueryable<Specialization> Specialization { get; }

    }
}
