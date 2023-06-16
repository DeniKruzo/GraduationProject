namespace GraduationProject.Models
{
    public class Chat
    {
        public long Id { get; set; }

        public ICollection<Message> Messages {get; set;}


    }

    public class Message
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
