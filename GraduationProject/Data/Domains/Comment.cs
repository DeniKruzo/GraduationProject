using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Data.Domains
{
    public class Comment
    {
        [Key]
        public long CommentId { get; set; }

        public bool IsPositive { get; set; }

        public string OwnerId { get; set; }

        public long ProfileId { get; set; }

        public virtual Profile Profiles { get; set; }

        [Required]
        [Display(Name = "Текст комментария")]
        public string Text { get; set; }

    }
}
