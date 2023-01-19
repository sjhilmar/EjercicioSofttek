using EjercicioSofttek.Data;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioSofttek.Controllers
{
    public class GenerarTokenController : ControllerBase 
    {
        private readonly VentasContext context;

        public GenerarTokenController(VentasContext context)
        {
            this.context= context;  
        
        }




        
    }
}
