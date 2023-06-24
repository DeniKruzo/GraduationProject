using GraduationProject.Data.Domains;
using GraduationProject.Domains;

namespace GraduationProject.Models
{
    public class UpdateResponseModel : Response
    {
        public IQueryable<openOrder> openOrder { get; set; }

        public IQueryable<Response> Response { get; set; }

        public IQueryable<Profile> Profile { get; set; }
    }
}
