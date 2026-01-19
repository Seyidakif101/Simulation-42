using System.ComponentModel.DataAnnotations;

namespace Simulation_42.ViewModels.CategoryViewModels
{
    public class CategoryCreateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(256), MinLength(3)]
        public string Name { get; set; } = string.Empty;
    }
}
