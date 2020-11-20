using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Axosnet.Recibos.Seguridad.Token
{
    public class TokenGenerador : ITokenGenerador
    {
        private readonly IOptions<TokenConfiguracion> _configs;


        public TokenGenerador(IOptions<TokenConfiguracion> configs)
        {
            _configs = configs;
        }


        public string CrearToken(string email, Guid id)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, email),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs.Value.key));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var expiracion = DateTime.Now.AddDays(30);

            var tokenDescripcion = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credenciales
            };

            var tokenManejador = new JwtSecurityTokenHandler();
            var token = tokenManejador.CreateToken(tokenDescripcion);

            return tokenManejador.WriteToken(token);
        }
    }
}
