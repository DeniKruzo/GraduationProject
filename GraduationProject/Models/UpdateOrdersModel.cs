using GraduationProject.Data;
using GraduationProject.Domains;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GraduationProject.Models
{
    public class UpdateOrdersModel : openOrder
    {
        public SelectList SelectedCat { get; set; }

        public string GetOrderName { get; set; }

        public openOrder openOrder { get; set; }

        [Display(Name = "Картинка(по желанию)")]
        public IFormFile formFile { get; set; }
      
    }
}
