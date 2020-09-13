using AutoMapper;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Queries
{
    public class PedidoQueries : IPedidoQueries
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;
        public PedidoQueries(IPedidoRepository droneRepository, IMapper mapper)
        {
            _pedidoRepository = droneRepository;
            _mapper = mapper;
        }


        public async Task<PedidoViewModel> ObterPorId(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPorId(id));
        }
        public async Task<IEnumerable<PedidoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterTodos());
        }

        public async Task<IEnumerable<PedidoViewModel>> ObterPedidosEmTransito()
        {
            return _mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterTodos());
        }

        public async Task<IEnumerable<PedidoViewModel>> ObterPedidosEmTransitoPorDrone(Guid drone)
        {
            return _mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterTodos());
        }
        public async Task<IEnumerable<PedidoViewModel>> ObterPedidosEmAberto()
        {

            //TODO: ordenar FILA = primeiro a chegar deve ser o primeiro a sair!!!

            return _mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterTodos());
        }
        
    }
}
