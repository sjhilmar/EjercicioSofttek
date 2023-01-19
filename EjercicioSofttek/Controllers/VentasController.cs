using EjercicioSofttek.Data;
using EjercicioSofttek.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                venta.Add(new Ventas { fecha = DateTime.Now, cliente = "Primer cliente", vendedor = "Vendedor 1", importe = 1500, producto = "LapTop" });
                venta.Add(new Ventas { fecha = DateTime.Now, cliente = "Segundo cliente", vendedor = "Vendedor 1", importe = 8500, producto = "Servidor" });
                venta.Add(new Ventas { fecha = DateTime.Now, cliente = "Tercer cliente", vendedor = "Vendedor 2", importe = 750, producto = "Monitor" });
                context.Ventas.AddRange(venta);
                context.SaveChanges();  

            }
                
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var listarVentas = await context.Ventas.ToListAsync();
            return Ok(listarVentas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GeTById(int id)
        {
            var listarVentas = await context.Ventas.FirstOrDefaultAsync(s  => s.Id == id);
            if (listarVentas == null)
            {
                return NotFound();
            }  else { 
                return Ok(listarVentas); 
            }    
            
        }

        [HttpPost]
        public async Task<ActionResult> Add(Ventas ventas)
        {
           context.Ventas.Add(ventas);
           await   context.SaveChangesAsync();
            return CreatedAtAction(nameof(GeTById), new { id = ventas.Id }, ventas); 

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Ventas ventas)        {
            
            if (id == ventas.Id)
            {
                context.Entry(ventas).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest();

        }
         [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ventas = await context.Ventas.FindAsync(id);
            if (ventas == null)
            {
                return NotFound();
            }
            context.Ventas.Remove(ventas);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
