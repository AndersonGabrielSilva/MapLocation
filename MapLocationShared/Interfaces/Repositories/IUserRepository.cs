using MapLocationShared.Entities;
using MapLocationShared.Model.Account;
using System.Threading.Tasks;

namespace MapLocationShared.Interfaces.Repositories
{
    public interface IUserRepository : ICrudCommands<User>
    {
        public Task<User> LoginAsync(UserLogin userLogin);
    }
}
