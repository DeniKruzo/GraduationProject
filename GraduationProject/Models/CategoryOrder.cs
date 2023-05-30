using GraduationProject.Domains;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GraduationProject.Models
{
    public class CategoryOrder
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Название категории")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        public List<openOrder> Orders { get; set; }

    }
}
