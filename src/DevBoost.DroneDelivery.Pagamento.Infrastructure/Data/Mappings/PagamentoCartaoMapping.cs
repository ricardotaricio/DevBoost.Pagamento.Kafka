using DevBoost.DroneDelivery.Pagamento.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevBoost.DroneDelivery.Pagamento.Infrastructure.Data.Mappings
{
    public class PagamentoCartaoMapping : IEntityTypeConfiguration<PagamentoCartao>
    {
        public void Configure(EntityTypeBuilder<PagamentoCartao> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PedidoId).IsRequired();
            builder.Property(p => p.Valor).IsRequired();
            builder.Property(p => p.Situacao);


            builder.HasOne(c => c.Cartao)
                .WithOne(p => p.PagamentoCartao).HasForeignKey<PagamentoCartao>(c => c.CartaoId);

            builder.ToTable("Pagamento");
        }
    }
}
