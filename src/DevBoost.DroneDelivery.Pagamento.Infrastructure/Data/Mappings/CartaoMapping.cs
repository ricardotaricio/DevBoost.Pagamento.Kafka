using DevBoost.DroneDelivery.Pagamento.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevBoost.DroneDelivery.Pagamento.Infrastructure.Data.Mappings
{
    public class CartaoMapping : IEntityTypeConfiguration<Cartao>
    {
        public void Configure(EntityTypeBuilder<Cartao> builder)
        {
            builder.HasKey(c=> c.Id);

            builder.Property(c => c.Numero).HasColumnType("Varchar(50)").IsRequired();
            builder.Property(c => c.Bandeira).HasColumnType("Varchar(30)").IsRequired();
            builder.Property(c => c.MesVencimento).IsRequired();
            builder.Property(p => p.AnoVencimento).IsRequired();
            
            builder.HasOne(c => c.PagamentoCartao)
                .WithOne(p => p.Cartao);
            
            builder.ToTable("Cartao");
        }
    }
}
