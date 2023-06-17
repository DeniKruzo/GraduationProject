using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class Message
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime TimeStamp { get; set; }

        public long ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}
