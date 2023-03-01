using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class AddressDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Fill in address")]
        public string AddressContent { get; set; }
        [Required(ErrorMessage = "Fill in email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Fill in phone")]
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        [Required(ErrorMessage = "Fill in map")]
        public string LargeMapPath { get; set; }
        [Required(ErrorMessage = "Fill in map")]
        public string SmallMapPath { get; set; }
    }
}