using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DTO
{
    public class SocialMediaDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please fill in name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please fill in link")]

        public string Link { get; set; }

        public string ImagePath { get; set; }
        [Display(Name = "Image")]
        public HttpPostedFileBase SocialImage { get; set; }
    }
}