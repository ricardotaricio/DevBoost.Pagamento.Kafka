using DevBoost.DroneDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Mappings
{
    public class DroneItinerarioMapping : IEntityTypeConfiguration<DroneItinerario>
    {
        public void Configure(EntityTypeBuilder<DroneItinerario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(i => i.DataHora).IsRequired();
            builder.Property(i => i.Controle).UseIdentityColumn();
            builder.Property(i => i.StatusDrone).HasColumnType("int");
            

            builder.ToTable("DroneItinerario");
        }

    }
}
