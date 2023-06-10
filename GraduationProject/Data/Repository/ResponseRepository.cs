using GraduationProject.Abstract;
using GraduationProject.Data.Domains;

namespace GraduationProject.Data.Repository
{
    public class ResponseRepository : IMakeResponse
    {
        private readonly GraduationDbContext appDbContext;

        public ResponseRepository(GraduationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IQueryable<Response> Responses => appDbContext.Responses;
    }
}
