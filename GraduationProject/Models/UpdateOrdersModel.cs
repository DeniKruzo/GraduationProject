using GraduationProject.Data;
using GraduationProject.Domains;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace GraduationProject.Models
{
    public class UpdateOrdersModel : openOrder
    {
        public SelectList SelectedCat { get; set; }

        public string GetOrderName { get; set; }

        public openOrder openOrder { get; set; }

        [Display(Name = "Картинка(по желанию)")]
        [NotMapped]
        public IFormFile formFile { get; set; }

        public UpdateOrdersModel()
        {

        }

        public UpdateOrdersModel(openOrder entity)
        {
            OrderId = entity.OrderId;
            Name = entity.Name;
            ShortDesc = entity.ShortDesc;
            LongDesc = entity.LongDesc;
            Img = entity.Img;
            Price = entity.Price;
            CategoryID = entity.CategoryID;
            CategoryOrder = entity.CategoryOrder;
            isOpen = entity.isOpen;
            CustomerId = entity.CustomerId;
            DeadLine = entity.DeadLine;
        }
      
    }
}
