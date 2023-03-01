using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class MetaDTO
    {
        public int MetaID { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Meta Content is required")]
        public string MetaContent { get; set; }
    }
}