using AutoMapper;
using DevBoost.DroneDelivery.Pagamento.Application.DTOs;
using DevBoost.DroneDelivery.Pagamento.Domain.Entites;

namespace DevBoost.DroneDelivery.Pagamento.Infrastructure.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {

        public DomainToDtoMappingProfile()
        {
            CreateMap<PagamentoCartao, PagamentoRequestDTO>()
                .ForMember(d => d.NumeroCartao, o => o.MapFrom(o => o.Cartao.Numero))
                .ForMember(d => d.Bandeira, o => o.MapFrom(o => o.Cartao.Bandeira))
                .ForMember(d => d.AnoVencimento, o => o.MapFrom(o => o.Cartao.AnoVencimento))
                .ForMember(d => d.MesVencimento, o => o.MapFrom(o => o.Cartao.MesVencimento));
              
        }
    }
}
