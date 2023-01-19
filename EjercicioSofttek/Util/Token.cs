using EjercicioSofttek.Data;
using System.Security.Claims;

namespace EjercicioSofttek.Util
{
    public static class Token
    {

        public static dynamic validarToken(ClaimsIdentity identity, VentasContext context)
        {
            try
            {
                if (identity.Claims.Count()==0)
                {
                    return new
                    {
                        success = false,
                        message = "Verificar si estas enviando un token valido",
                        result=""
                    };
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "Id").Value;
                var asesor = context.asesorComercials.Find(Convert.ToInt32( id));

                return new
                {
                    success = true,
                    message = "Exito",
                    result = asesor.ToString()
                };

            }
            catch (Exception e)
            {
               
                return new 
                {
                    success= false,
                    messsage="Catch: " + e.Message,
                    result=""
                };   

            }
        }
    }
}
