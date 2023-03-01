using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class CategoryDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }
    }
}