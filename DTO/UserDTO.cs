
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DTO
{
    public class UserDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter your username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        public string Email { get; set; }
        public string Imagepath { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        public bool isAdmin { get; set; }
        [Display(Name = "User Image")]
        public HttpPostedFileBase UserImage { get; set; }
    }
}