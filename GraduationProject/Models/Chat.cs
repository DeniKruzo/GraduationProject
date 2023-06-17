using GraduationProject.Areas.Identity.Data;

namespace GraduationProject.Models
{
    public class Chat
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ICollection<Message> Messages {get; set;}

        public ICollection<ChatUser> Users { get; set;}

        public ChatType Type { get; set;}
    }
}
