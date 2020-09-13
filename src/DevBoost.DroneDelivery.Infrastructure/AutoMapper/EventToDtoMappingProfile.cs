using AutoMapper;
using DevBoost.DroneDelivery.Application.Events;
using DevBoost.DroneDelivery.Domain.Entities;

namespace DevBoost.DroneDelivery.Infrastructure.AutoMapper
{
    public class EventToDtoMappingProfile : Profile
    {

        public EventToDtoMappingProfile()
        {
            CreateMap<Drone,DroneAdicionadoEvent> ()
                .ConstructUsing(d =>  new DroneAdicionadoEvent(d.Id))               
                   .ReverseMap();
        }

    }
}
