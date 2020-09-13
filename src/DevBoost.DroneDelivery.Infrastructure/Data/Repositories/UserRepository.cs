using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Extensions;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<Usuario>, IUserRepository
    {
        private readonly DCDroneDelivery _context;

        public UserRepository(DCDroneDelivery context, IMediatrHandler bus) : base(context)
        {
            _context = context;
        }

        public async Task<Usuario> ObterPorNome(string username)
        {
            return await _context.User
                .Where(u => u.UserName == username)
                .Include(u => u.Cliente)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuario> ObterCredenciais(string username, string password)
        {
            return await _context.User.AsNoTracking().Where(u => u.UserName == username && u.Password == password.ObterHash()).FirstOrDefaultAsync();
        }

    }
}
