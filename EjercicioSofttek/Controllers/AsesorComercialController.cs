using EjercicioSofttek.Data;
using EjercicioSofttek.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EjercicioSofttek.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AsesorComercialController : ControllerBase
    {

        private readonly VentasContext context;
        public AsesorComercialController(VentasContext context)
        {
            this.context = context;
            if (context.asesorComercials.Count() == 0)
            {
                List<AsesorComercial> asesor =new List<AsesorComercial>();
                asesor.Add(new AsesorComercial {descripcion="Asesor1" });
                context.asesorComercials.AddRange(asesor);
                context.SaveChanges();
            }
        }

        [HttpGet]
        
        public async Task<ActionResult>GetAll()
        {
            var listarAsesor = await context.asesorComercials.ToListAsync();
            return Ok(listarAsesor);    
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var listarAsesor = await context.asesorComercials.FirstOrDefaultAsync(s => s.Id == id);
            if (listarAsesor == null) return NotFound();
            return Ok(listarAsesor);
        }

        [HttpPost]
        public async Task<ActionResult> Save(AsesorComercial asesor)
        {
            context.asesorComercials.Add(asesor);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = asesor.Id }, asesor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult>Update(int id, AsesorComercial asesor)
        {
            if (id == asesor.Id)
            {
                context.Entry(asesor).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var asesor = await context.asesorComercials.FindAsync(id);
            if(asesor == null) return NotFound();   
            context.asesorComercials.Remove(asesor);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
