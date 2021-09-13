using MapLocationShared.Entities;
using MapLocationShared.Model.Account;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MapLocationShared.Services
{
    public static class TokenService
    {     
        public static string GenerateToken(User user, string Key)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var expiraEm = DateTime.UtcNow.AddDays(1).Date;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity claimsPrincipal = new ClaimsIdentity(new Claim[]
               {
                    new Claim(nameof(user.Name), user.Name)
               });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsPrincipal,
                Expires = expiraEm,
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
           
            return tokenHandler.WriteToken(token);
        }

        public static UserAuthenticated GenerateUserAutenticated(User user, string Key)
        {
            return new UserAuthenticated()
            {
                Token = GenerateToken(user, Key),

                Expire = DateTime.UtcNow.AddDays(1).Date
            };
        }
    }
}
