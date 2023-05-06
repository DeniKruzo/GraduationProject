using GraduationProject.Areas.Identity.Data;
using GraduationProject.Models;
using Microsoft.AspNetCore.Identity;
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
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortDesc { get; set; }

        public string LongDesc { get; set; }

        public string Img { get; set; }

        public ushort Price { get; set; }

        public int CategoryID { get; set; }

        public virtual CategoryOrder CategoryOrder { get; set; }

        public bool isOpen { get; set; }

        public string CustomerId { get; set; }

        public DateTime DeadLine { get; set; } 
    }
}
