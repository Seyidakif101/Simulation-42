using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulation_42.Models;

namespace Simulation_42.Configuration
{
    public class ProjectConfiguartion : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(1000);
            builder.HasOne(x => x.Category).WithMany(x => x.Projects).HasForeignKey(x => x.CategoryId).HasPrincipalKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
