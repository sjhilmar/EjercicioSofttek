using EjercicioSofttek.Data;
using EjercicioSofttek.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EjercicioSofttek.Controllers
{
    [ApiController]
    [Route("api/Token")]
    public class GenerarTokenController : ControllerBase 
    {
        private readonly VentasContext context;

        public IConfiguration configuration; 

        public GenerarTokenController(VentasContext context, IConfiguration configuration)
        {
            this.context= context;
            this.configuration = configuration;

        }
        
        [HttpPost]
        public dynamic IniciarSesion([FromBody] Object optData)
        {

            var data = JsonConvert.DeserializeObject<dynamic> (optData.ToString ());    
            int id = data.id ;
            string descripcion = data.descripcion;

            //var asesor = context.asesorComercials.Where(x => x.Id == id).FirstOrDefault();
            var asesor = context.asesorComercials.Find(id);
            if (asesor == null)
            {
                return new
                {
                    success = false,
                    message = "Credenciales incorrectas",
                    result=""
                };
            }
            var jwt = configuration.GetSection("Jwt").Get<Jwt>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id",asesor.Id.ToString()),
                new Claim ("descripcion",asesor.descripcion )
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires :DateTime.Now.AddMinutes(60),
                signingCredentials: signIn
                );

            return new
            {
                success = true,
                message = "Exito",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }


        
    }
}
