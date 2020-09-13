using AutoMapper;
using DevBoost.DroneDelivery.Pagamento.Application.Events;
using DevBoost.DroneDelivery.Pagamento.Application.ViewModels;

namespace DevBoost.DroneDelivery.Pagamento.Infrastructure.AutoMapper
{
    public class EventToDtoMappingProfile : Profile
    {
        public EventToDtoMappingProfile()
        {
            CreateMap<PagamentoCartaoAdicionadoEvent, AtualizarSituacaoPedidoViewModel>()
                .ForMember(d => d.PedidoId, o => o.MapFrom(o => o.EntityId));
        }
    }
}
