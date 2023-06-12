using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Data.Domains
{
    public class Response
    {
        [Key]
        public long ResponseId { get; set; }

        public string SenderId { get; set; }

        public string RecipientId { get; set;}

        public long ProfileOrOrderId { get; set; }

        [Required]
        [Display(Name = "Сообщение и контакты")]
        public string Message { get; set;}

    }
}
