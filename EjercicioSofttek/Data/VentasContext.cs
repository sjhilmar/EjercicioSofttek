using EjercicioSofttek.Models;
using Microsoft.EntityFrameworkCore;

namespace EjercicioSofttek.Data
{
    public class VentasContext : DbContext
    {
        public VentasContext(DbContextOptions<VentasContext> options) 
            :base(options)
        { 
        
        }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<AsesorComercial> asesorComercials { get; set; }    
        
    }
}
