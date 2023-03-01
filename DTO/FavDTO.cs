using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DTO
{
    public class FavDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Fav { get; set; }
        public string Logo { get; set; }
        [Display(Name = "LogoImage")]
        public HttpPostedFileBase LogoImage { get; set; }
        [Display(Name = "FavImage")]
        public HttpPostedFileBase FavImage { get; set; }
    }
}