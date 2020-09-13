using AutoMapper;
using DevBoost.DroneDelivery.Core.Domain.Enumerators;
using DevBoost.DroneDelivery.Pagamento.Application.Commands;
using DevBoost.DroneDelivery.Pagamento.Domain.Entites;

namespace DevBoost.DroneDelivery.Pagamento.Infrastructure.AutoMapper
{
    public class CommandToDomainMappingProfile : Profile
    {
        public CommandToDomainMappingProfile()
        {
            CreateMap<AdicionarPagamentoCartaoCommand, PagamentoCartao>()
                .ForMember(d => d.Cartao,
                o => o.MapFrom(o => new Cartao()
                {
                    Numero = o.NumeroCartao,
                    MesVencimento = o.MesVencimentoCartao,
                    AnoVencimento = o.AnoVencimentoCartao,
                    Bandeira = o.BandeiraCartao,

                }))
                .ForMember(d => d.Situacao, o => o.MapFrom(o => SituacaoPagamento.Aguardando));
        }

    }
}
