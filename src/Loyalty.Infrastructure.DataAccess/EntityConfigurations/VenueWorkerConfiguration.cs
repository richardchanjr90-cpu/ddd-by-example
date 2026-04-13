using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.DataAccess.EntityConfigurations
{
    public class VenueWorkerConfiguration : IEntityTypeConfiguration<VenueWorker>
    {
        public void Configure(EntityTypeBuilder<VenueWorker> builder)
        {
            builder.Ignore(b => b.DomainEvents);
            builder.Ignore(b => b.Id);
            builder.ToTable("VenueWorker", SchemaName.Loyalty);

            builder
                .HasKey(bc => new { bc.VenueId, bc.WorkerId });

            builder
                .HasOne(bc => bc.Venue)
                .WithMany(b => b.Workers)
                .HasForeignKey(bc => bc.VenueId);

            builder
                .HasOne(bc => bc.Worker)
                .WithMany(c => c.Venues)
                .HasForeignKey(bc => bc.WorkerId);

            builder
                .HasIndex(p => new { p.VenueId, p.WorkerId })
                .IsUnique();
        }
    }
}
