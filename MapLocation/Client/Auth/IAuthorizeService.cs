using System.Threading.Tasks;

namespace MapLocation.Client.Auth
{
    public interface IAuthorizeService
    {
        Task Login(string token);
        Task Logout();
    }
}
