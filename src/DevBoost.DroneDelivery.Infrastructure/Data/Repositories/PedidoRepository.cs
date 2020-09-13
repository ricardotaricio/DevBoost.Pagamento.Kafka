using DevBoost.Dronedelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Repositories
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        private readonly DCDroneDelivery _context;

        public PedidoRepository(DCDroneDelivery context) :base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> ObterPedidosEmAberto()
        {
            return await _context.Pedido
                .Include(p => p.Cliente).AsNoTracking()
                .Where(p => p.Status == EnumStatusPedido.AguardandoEntregador).ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> ObterPedidosEmTransito()
        {
            return await _context.Pedido
                .Include(p => p.Cliente).AsNoTracking()
                .Include(p => p.Drone).AsNoTracking()
                .Where(p => p.Status == EnumStatusPedido.EmTransito)
                .ToListAsync();
        }

       
    }
}
