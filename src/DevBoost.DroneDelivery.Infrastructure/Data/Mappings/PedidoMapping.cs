using DevBoost.DroneDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {

        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(c => c.Id);

            
            builder.Property(c => c.DataHora).IsRequired();
            builder.Property(c => c.Peso).IsRequired();
            builder.Property(c => c.Valor).IsRequired();

            builder.ToTable("Pedido");
        }
    }
}
