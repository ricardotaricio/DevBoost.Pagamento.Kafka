using DevBoost.DroneDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Mappings
{
    public class DroneMapping : IEntityTypeConfiguration<Drone>
    {
        public void Configure(EntityTypeBuilder<Drone> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Capacidade).IsRequired();
            builder.Property(c => c.Velocidade).IsRequired();
            builder.Property(c => c.Autonomia).IsRequired();
            builder.Property(c => c.Autonomia);
            builder.Property(c => c.Carga).IsRequired();

            builder.ToTable("Drone");
        }
    }
}
