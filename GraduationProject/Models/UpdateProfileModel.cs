using GraduationProject.Data.Domains;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace GraduationProject.Models
{
    public class UpdateProfileModel : Profile
    {
        [NotMapped]
        public SelectList SelectedSpec { get; set; }

        [NotMapped]
        public string GetProfileName { get; set; }

        [NotMapped]
        public Profile Profile { get; set; }

        [Display(Name = "Ваш аватар")]
        [NotMapped]
        public IFormFile formFile { get; set; }

        public UpdateProfileModel()
        {

        }

        public UpdateProfileModel(Profile prof)
        {
            ProfileId = prof.ProfileId;
            OwnerId = prof.OwnerId;
            IsFree = prof.IsFree;
            IsSafeDeal = prof.IsSafeDeal;
            AboutMe = prof.AboutMe;
            Services = prof.Services;
            AvatarImg = prof.AvatarImg;
            Rating = prof.Rating;
            LastVisit = prof.LastVisit;
            SpecId = prof.SpecId;
            SpecProfile = prof.SpecProfile;
            Comments = prof.Comments;
        }
    }
}
