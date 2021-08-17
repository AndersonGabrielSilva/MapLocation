using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapLocation.Client.Auth
{
    public interface IAuthorizeService
    {
        Task Login(string token);
        Task Logout();
    }
}
