using GraduationProject.Data.Domains;
using GraduationProject.Domains;

namespace GraduationProject.Abstract
{
    public interface IGetProfiles
    {
        IQueryable<Profile> Profile { get; }

        Profile GetObjectProfile(long ProfileId);
    }
}
