using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DTO
{
    public class AdsDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Link is required")]
        public string Link { get; set; }
        [Required(ErrorMessage = "Size is required")]
        public string Imagesize { get; set; }
        [Display(Name = "Ads Image")]
        public HttpPostedFileBase AdsImage { get; set; }
    }
}