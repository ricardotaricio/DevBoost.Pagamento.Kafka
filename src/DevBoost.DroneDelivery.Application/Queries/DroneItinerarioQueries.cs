using AutoMapper;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Queries
{
    public class DroneItinerarioQueries: IDroneItinerarioQueries
    {
        private readonly IDroneItinerarioRepository _droneItinerarioRepository;
        private readonly IMapper _mapper;
        public DroneItinerarioQueries(IDroneItinerarioRepository droneItinerarioRepository, IMapper mapper)
        {
            _droneItinerarioRepository = droneItinerarioRepository;
            _mapper = mapper;
        }


        public async Task<DroneItinerarioViewModel> ObterPorId(Guid id)
        {
            return _mapper.Map<DroneItinerarioViewModel>(await _droneItinerarioRepository.ObterPorId(id));
        }
        public async Task<IEnumerable<DroneItinerarioViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<DroneItinerario>, IEnumerable<DroneItinerarioViewModel>>(await _droneItinerarioRepository.ObterTodos());
        }
        public async Task<IEnumerable<DroneItinerarioViewModel>> ObterDronesIndisponiveis()
        {
            return _mapper.Map<IEnumerable<DroneItinerario>, IEnumerable<DroneItinerarioViewModel>>(await _droneItinerarioRepository.ObterTodos());
        }
        public async Task<IEnumerable<DroneViewModel>> ObterDronesDisponiveis()
        {
            
            return _mapper.Map<IEnumerable<DroneItinerario>, IEnumerable<DroneViewModel>>(await _droneItinerarioRepository.ObterTodos());
        }
    }
}
