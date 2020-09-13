using AutoMapper;
using DevBoost.DroneDelivery.Pagamento.Application.Commands;
using DevBoost.DroneDelivery.Pagamento.Application.ViewModels;

namespace DevBoost.DroneDelivery.Pagamento.Infrastructure.AutoMapper
{
    public class ViewModelToCommandMappingProfile : Profile
    {
        public ViewModelToCommandMappingProfile()
        {
            CreateMap<AdicionarPagamentoCartaoViewModel, AdicionarPagamentoCartaoCommand>()
                .ConstructUsing(p => new AdicionarPagamentoCartaoCommand(p.PedidoId,p.Valor,p.BandeiraCartao,p.NumeroCartao,p.MesVencimentoCartao,p.AnoVencimentoCartao));
        }
    }
}
