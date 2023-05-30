using GraduationProject.Areas.Identity.Data;
using GraduationProject.Data.Domains;
using GraduationProject.Domains;

namespace GraduationProject.Models.ViewModel
{
    public class ProfileListViewModel
    {
        public IEnumerable<Profile> getAllProfiles { get; set; }

        public IEnumerable<Specialization> getSpecialization { get; set; }

        public IEnumerable<ApplicationUser> getUsers { get; set; }
    }
}
