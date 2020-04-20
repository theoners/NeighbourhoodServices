namespace NeighbourhoodServices.Web.ViewModels.Announcements
{
    using System.ComponentModel.DataAnnotations;

    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;

    public class AnnouncementInputModel : IMapFrom<Announcement>
    {
        [Required(ErrorMessage = "Заглавие е задължително")]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Тема е задължително")]
        [Display(Name = "Тема")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Цената е задължително")]
        [Display(Name = "Цена")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Поле Място е задължително")]
        [Display(Name = "Място")]
        public string Place { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        public ServiceType ServiceType { get; set; }

        public string UserId { get; set; }
    }
}
