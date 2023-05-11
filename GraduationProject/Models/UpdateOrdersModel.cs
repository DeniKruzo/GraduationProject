using GraduationProject.Data;
using GraduationProject.Domains;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Models
{
    public class UpdateOrdersModel : openOrder
    {
        public SelectList SelectedCat { get; set; }

        public string GetOrderName { get; set; }

        public openOrder openOrder { get; set; }

    
      
    }
}
