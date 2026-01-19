using System.ComponentModel.DataAnnotations;

namespace Simulation_42.ViewModels.CategoryViewModels
{
    public class CategoryGetVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
