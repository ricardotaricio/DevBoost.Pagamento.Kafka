using AutoMapper;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Queries
{
    public class DroneQueries : IDroneQueries
    {
        private readonly IDroneRepository  _droneRepository;
        private readonly IMapper _mapper;
        public DroneQueries(IDroneRepository  droneRepository, IMapper mapper)
        {
            _droneRepository = droneRepository;
            _mapper = mapper;
        }


        public async Task<DroneViewModel> ObterPorId(Guid id)
        {
            return _mapper.Map<DroneViewModel>(await _droneRepository.ObterPorId(id));
        }
        public async Task<IEnumerable<DroneViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<Drone>, IEnumerable<DroneViewModel>>(await _droneRepository.ObterTodos());
        }

        public async Task<IEnumerable<DroneViewModel>> ObterSituacao()
        {
            return _mapper.Map<IEnumerable<Drone>, IEnumerable<DroneViewModel>>(await _droneRepository.ObterTodos());
        }

    }
}
