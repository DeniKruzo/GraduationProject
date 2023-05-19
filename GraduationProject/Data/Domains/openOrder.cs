using GraduationProject.Areas.Identity.Data;
using GraduationProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace GraduationProject.Domains
{
    /// <summary>
    /// Предметная область для Заданий на сайте
    /// На сайте будут выдываться открытые задания с временем до
    /// которого они должны выдаваться.
    /// </summary>
    public class openOrder
    {
        [Key]
        public long OrderId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Короткое описание")]
        public string ShortDesc { get; set; }

        [Display(Name = "Полное описание")]
        public string LongDesc { get; set; }

        [Display(Name = "Картинка")]
        public string Img { get; set; }

        [Display(Name = "Бюджет")]
        public ushort Price { get; set; }

        [Display(Name = "Категория")]
        public int CategoryID { get; set; }

        [Display(Name = "Категория")]
        public virtual CategoryOrder CategoryOrder { get; set; }

        [Display(Name = "Актуальность")]
        public bool isOpen { get; set; }

        [Display(Name = "Заказчик")]
        public string CustomerId { get; set; }

        [Display(Name = "Дата окончания")]
        public DateTime DeadLine { get; set; } 
    }
}
