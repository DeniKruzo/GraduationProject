using GraduationProject.Areas.Identity.Data;

namespace GraduationProject.Models
{
    public class ChatUser
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public long ChatId { get; set; }

        public Chat Chat { get; set; }

        public UserRoleInChat RoleInChat { get; set; }
    }
}
