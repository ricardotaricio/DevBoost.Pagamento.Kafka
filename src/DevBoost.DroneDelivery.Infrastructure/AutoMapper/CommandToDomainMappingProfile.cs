using AutoMapper;
using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Domain.Entities;

namespace DevBoost.DroneDelivery.Infrastructure.AutoMapper
{
    public class CommandToDomainMappingProfile : Profile
    {
        public CommandToDomainMappingProfile()
        {
            CreateMap<AdicionarUsuarioCommand, Usuario>()
                .ConstructUsing(u => new Usuario(u.UserName, u.Password,u.Role, u.ClienteId)).ReverseMap();

            CreateMap<AdicionarClienteCommand,Cliente>()
                .ConstructUsing(c => new Cliente(c.Nome,c.Latitude,c.Longitude)).ReverseMap();
           
            CreateMap<AdicionarClienteCommand, Usuario>()
                .ConstructUsing(c => new Usuario(c.Usuario,c.Senha,string.Empty,c.Id)).ReverseMap();

            CreateMap<AdicionarDroneCommand, Drone>()
                .ConstructUsing(d => new Drone(d.Capacidade,d.Velocidade,d.Autonomia,d.AutonomiaRestante)).ReverseMap();

            CreateMap<AdicionarDroneItinerarioCommand, DroneItinerario>()
                .ConstructUsing(i => new DroneItinerario(i.DataHora,i.DroneId,i.StatusDrone)).ReverseMap();

        }

    }
}
