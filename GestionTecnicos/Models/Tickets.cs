using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionTecnicos.Models;


public class Tickets
{
    [Key]
    public int TicketId { get; set; }
    
    [Required]
    public DateTime Fecha { get; set; } = DateTime.Now;
    
    [Required]
    public string Prioridad { get; set; }
    
    [Required]
    public int ClienteId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Asunto { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Descripcion { get; set; }
    
    [Required]
    public double TiempoInvertido { get; set; } 

    [ForeignKey("ClienteId")] 
    public Clientes Cliente { get; set; } = null!;


}