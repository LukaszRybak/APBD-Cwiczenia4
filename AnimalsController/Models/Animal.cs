using System.ComponentModel.DataAnnotations;

namespace AnimalsController.Models
{
    public class Animal
    {
        public int Id{ get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description{ get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Area is required")]
        public string Area { get; set; }

    }
}
