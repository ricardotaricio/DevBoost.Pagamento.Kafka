using DevBoost.DroneDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserName).HasColumnType("Varchar(50)").IsRequired();
            builder.Property(c => c.Password).HasColumnType("Varchar(max)").IsRequired();
            builder.Property(c => c.Role).IsRequired();

            builder.HasOne(c => c.Cliente);
                
            builder.ToTable("Usuario");
        }
    }
}
