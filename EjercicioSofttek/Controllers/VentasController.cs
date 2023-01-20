using EjercicioSofttek.Data;
using EjercicioSofttek.Models;
using EjercicioSofttek.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EjercicioSofttek.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class VentasController : ControllerBase
    {
        private readonly VentasContext context;
        public  VentasController(VentasContext context)
        {
            this.context = context; 
            if (context .Ventas.Count() == 0)
            {
                List<Ventas> venta = new List<Ventas>();
                venta.Add(new Ventas { fecha = DateTime.Now, cliente = "Primer cliente", vendedor = "Asesor1", producto = "LapTop" ,cantidad=1 ,precio=1500 , importe = 1500 });
                venta.Add(new Ventas { fecha = DateTime.Now, cliente = "Segundo cliente", vendedor = "Asesor1", producto = "Servidor", cantidad = 1, precio = 8500, importe = 8500 });
                venta.Add(new Ventas { fecha = DateTime.Now, cliente = "Tercer cliente", vendedor = "Asesor1", producto = "Monitor", cantidad = 1, precio = 750, importe = 750 });
                context.Ventas.AddRange(venta);
                context.SaveChanges();  

            }
                
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            
            var rToken = Token.validarToken(identity, context);
            if (rToken.success)
            {
                var listarVentas = await context.Ventas.ToListAsync();
                return Ok(listarVentas);
            }
            return NotFound();
            
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GeTById(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = Token.validarToken(identity, context);

            if (rToken.success)
            {
                var listarVentas = await context.Ventas.FirstOrDefaultAsync(s => s.Id == id);
                if (listarVentas == null) return NotFound();
                return Ok(listarVentas);
            }
            return NotFound();
            
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Save(Ventas ventas)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = Token.validarToken(identity, context);

            if (rToken.success)
            {
                context.Ventas.Add(ventas);
                await context.SaveChangesAsync();
                return CreatedAtAction(nameof(GeTById), new { id = ventas.Id }, ventas);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Update(int id, Ventas ventas)        {
            
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = Token.validarToken(identity, context);

            if (rToken.success)
            {
                if (id == ventas.Id)
                {
                    context.Entry(ventas).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return NoContent();
                }
                return BadRequest();

            }
            return BadRequest();
        }
         
        
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = Token.validarToken(identity, context);

            if (rToken.success)
            {
                var ventas = await context.Ventas.FindAsync(id);
                if (ventas == null) return NotFound();

                context.Ventas.Remove(ventas);
                await context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }

    }
}
