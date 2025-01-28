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
    [Range(1,4,ErrorMessage = "la prioridad debe ser entre 1 a 4")]
    public int Prioridad { get; set; }
    
    [Required]
    public int ClienteId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Asunto { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Descripcion { get; set; }
    
    public TimeSpan TiempoInvertido { get; set; } = TimeSpan.Zero;
    
    public int TecnicoId { get; set; }
    
    
    //TicketId, Fecha, Prioridad, ClienteId,Asunto, Descripcion, TiempoInvertido, TecnicoId.
}