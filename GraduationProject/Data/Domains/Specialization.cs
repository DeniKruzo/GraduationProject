using GraduationProject.Domains;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Data.Domains
{
    public class Specialization
    {
        [Key]
        public long SpecializationId { get; set; }

        [Display(Name = "Название специализации")]
        [Required]
        public string Name { get; set; }

        public List<Profile> Profiles { get; set; }
    }
}
