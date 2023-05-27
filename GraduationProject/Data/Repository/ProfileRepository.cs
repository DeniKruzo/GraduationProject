using GraduationProject.Abstract;
using GraduationProject.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Data.Repository
{
    public class ProfileRepository : IGetProfiles
    {
        private readonly GraduationDbContext appDbContext;

        public ProfileRepository(GraduationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public IQueryable<Profile> Profile => appDbContext.Profile.Include(s => s.SpecProfile);

        public Profile GetObjectProfile(long ProfileId) => appDbContext.Profile.FirstOrDefault(p => p.ProfileId == ProfileId);
       
    }
}
