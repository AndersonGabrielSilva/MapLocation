using MapLocationShared.Interfaces.Repositories;
using MapLocationShared.Model.Account;
using MapLocationShared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorGPS.Server.Controllers
{
    public class AccountController : BaseController
    {
        #region Atributos
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        #endregion

        #region Constructor
        public AccountController(IConfiguration configuration,
                                 IHttpContextAccessor contextAccessor,
                                 IUserRepository userRepository)
            : base(contextAccessor)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }
        #endregion

        public async Task<ActionResult<UserAuthenticated>> Login([FromBody] UserLogin User)
        {
            var userAut = await _userRepository.LoginAsync(User);

            if (userAut == null)
                return BadRequest(new UserAuthenticated { Message = "Usuário ou senha inválido" });

            var key = _configuration["jwt:key"];

            return TokenService.GenerateUserAutenticated(userAut, key);
        }

    }
}
