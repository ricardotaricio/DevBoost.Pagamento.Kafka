using AutoMapper;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Domain.Entities;

namespace DevBoost.DroneDelivery.Infrastructure.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteViewModel, Cliente>()
                .ConstructUsing(c => new Cliente(c.Nome, c.Latitude, c.Longitude)).ReverseMap();

            CreateMap<DroneViewModel, Drone>()
                .ConstructUsing(d => new Drone(d.Capacidade,d.Velocidade,d.Autonomia,d.AutonomiaRestante)).ReverseMap();

            CreateMap<UsuarioViewModel, Usuario>()
               .ConstructUsing(d => new Usuario(d.UserName,d.Password,d.Role,d.ClienteId)).ReverseMap();

            CreateMap<Cliente, ClienteViewModel>()
               .ForMember(d => d.Nome, o => o.MapFrom(o => o.Nome))
                .ForMember(d => d.Id, o => o.MapFrom(o => o.Id))
                .ForMember(d => d.Longitude, o => o.MapFrom(o => o.Longitude))
                .ForMember(d => d.Latitude, o => o.MapFrom(o => o.Latitude));
                
        }

    }
}
