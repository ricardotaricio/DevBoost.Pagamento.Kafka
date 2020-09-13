using AutoMapper;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Queries
{
    public class UsuarioQueries : IUsuarioQueries
    {
        private readonly IUserRepository  userRepository;
        private readonly IMapper _mapper;

        public UsuarioQueries(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioViewModel> ObterPorCredenciais(string username, string password)
        {
            return _mapper.Map<UsuarioViewModel>(await userRepository.ObterCredenciais(username,password));
        }
        public async Task<UsuarioViewModel> ObterPorNome(string username)
        {
            return _mapper.Map<UsuarioViewModel>(await userRepository.ObterPorNome(username));
        }

    }
}
