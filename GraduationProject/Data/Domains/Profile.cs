using GraduationProject.Models;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Data.Domains
{
    /// <summary>
    /// Анкета специалиста
    /// </summary>
    public class Profile
    {
        [Key]
        public long ProfileId { get; set; }

        public string OwnerId { get; set; }

        [Display(Name = "Свободен")]
        public bool IsFree { get; set; }

        [Display(Name = "Безопасная сделка")]
        public bool IsSafeDeal { get; set; }

        [Display(Name = "Обо мне")]
        [Required]
        public string AboutMe { get; set; }

        [Display(Name = "Какие услуги вы предлагаете")]
        [Required]
        public string Services { get; set; }

        [Display(Name = "Аватарка")]
        public string AvatarImg { get; set; }

        [Display(Name = "Рейтинг")]
        public short Rating { get; set; }

        [Display(Name = "Был в сети")]
        public DateTime LastVisit { get; set; }

        //для специализации 1 к м

        [Display(Name = "Специализация ид")]
        public long SpecId { get; set; }

        [Display(Name = "Специализация")]
        public virtual Specialization SpecProfile { get; set; }

        //для отзывов м к 1

        public List<Comment> Comments { get; set; }
    }
}
