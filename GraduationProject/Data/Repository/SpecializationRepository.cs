using GraduationProject.Abstract;
using GraduationProject.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Data.Repository
{
    public class SpecializationRepository : IHaveSpecialization
    {
        private readonly GraduationDbContext appDbContext;

        public SpecializationRepository(GraduationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IQueryable<Specialization> Specialization => appDbContext.Specialization;
     
    }
}
