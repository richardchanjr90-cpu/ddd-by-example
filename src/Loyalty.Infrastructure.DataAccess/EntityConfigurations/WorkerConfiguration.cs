using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Core.Entities.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.DataAccess.EntityConfigurations
{
    public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.ToTable("Worker", SchemaName.Loyalty);
            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder
                .HasIndex(u => u.Phone)
                .IsUnique();

            builder
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasFilter("([Email] IS NOT NULL)");

            builder
                .HasIndex(u => u.WorkerId)
                .IsUnique()
                .HasFilter("([IsArchived] = 0 AND WorkerId IS NOT NULL)");

            var navigation = builder.Metadata.FindNavigation(nameof(Worker.VenueRoles));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(o => o.Id)
                .UseHiLo("workereq");
        }
    }
}
