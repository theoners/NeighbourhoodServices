using System.Collections.Generic;
using NeighbourhoodServices.Web.ViewModels.Categories;

namespace NeighbourhoodServices.Web.ViewModels.Announcements
{
    using System.ComponentModel.DataAnnotations;

    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;

    public class AnnouncementInputModel : IMapFrom<Announcement>
    {
        public string Id { get; set; }
       
        [Required(ErrorMessage = "Тема е задължително")]
        [Display(Name = "Тема")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Описание е задължително")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Цената е задължително")]
        [Display(Name = "Цена")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Място е задължително")]
        [Display(Name = "Място")]
        public string Place { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        public string Category { get; set; }

        [Range(1, 3, ErrorMessage = "Вид услуга е задължително")]
        public ServiceType ServiceType { get; set; }

        public string UserId { get; set; }
       
        
    }
}
