using System.ComponentModel.DataAnnotations;

namespace Simulation_42.ViewModels.ProjectViewModels
{
    public class ProjectUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(256), MinLength(3)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public IFormFile? Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
