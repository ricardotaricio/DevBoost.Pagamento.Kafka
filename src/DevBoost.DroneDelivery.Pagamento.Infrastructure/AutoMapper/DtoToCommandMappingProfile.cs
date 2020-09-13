using AutoMapper;
using DevBoost.DroneDelivery.Core.Domain.Enumerators;
using DevBoost.DroneDelivery.Pagamento.Application.Commands;
using DevBoost.DroneDelivery.Pagamento.Application.DTOs;

namespace DevBoost.DroneDelivery.Pagamento.Infrastructure.AutoMapper
{
    public class DtoToCommandMappingProfile : Profile
    {
        public DtoToCommandMappingProfile()
        {
            CreateMap<PagamentoResponseDTO, AtualizarSituacaoPagamentoCartaoCommand>()
                .ConstructUsing(p => new AtualizarSituacaoPagamentoCartaoCommand(p.PagamentoId, p.Autorizado ? SituacaoPagamento.Autorizado : SituacaoPagamento.Negado));
                

        }
    }
}
