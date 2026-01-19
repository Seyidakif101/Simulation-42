using Simulation_42.Models.Common;

namespace Simulation_42.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Project> Projects { get; set; } = [];
    }
}
