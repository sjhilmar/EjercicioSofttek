using System.ComponentModel.DataAnnotations.Schema;
namespace EjercicioSofttek.Models
{
    public class Ventas
    {
        public int Id { get; set; }
        public DateTime fecha { get; set; }
        public string cliente { get; set; }
        public string vendedor { get; set; }    
        public string producto { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public double importe { get; set; } 



    }
}
