using AutoMapper;
using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Application.ViewModels;

namespace DevBoost.DroneDelivery.Infrastructure.AutoMapper
{
    public class ViewModelToCommandMappingProfile : Profile
    {

        public ViewModelToCommandMappingProfile()
        {
            CreateMap<AtualizarSituacaoPedidoViewModel, AtualizarSituacaoPedidoCommand>();
            CreateMap<AdicionarClienteViewModel, AdicionarClienteCommand>()
                .ForMember(d => d.Nome, o => o.MapFrom(o => o.Nome))
                .ForMember(d => d.Latitude, o => o.MapFrom(o => o.Latitude))
                .ForMember(d => d.Longitude, o => o.MapFrom(o => o.Longitude))
                .ForMember(d => d.Senha, o => o.MapFrom(o => o.Senha))
                .ForMember(d => d.Usuario, o => o.MapFrom(o => o.Usuario));


            CreateMap<AdicionarDroneViewModel, AdicionarDroneCommand>();

            CreateMap<AdicionarDroneViewModel,AdicionarDroneCommand>()
                .ConstructUsing(d => new AdicionarDroneCommand(d.Capacidade, d.Velocidade,d.Autonomia));

            
        }
    }
}
