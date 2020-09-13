using AutoMapper;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Queries
{
    public class ClienteQueries : IClienteQueries
    {

        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public ClienteQueries(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }


        public async Task<ClienteViewModel> ObterPorId(Guid id)
        {
          return _mapper.Map<ClienteViewModel>( await _clienteRepository.ObterPorId(id));
        }
        public async Task<IEnumerable<ClienteViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(await _clienteRepository.ObterTodos());
        }
    }
}
