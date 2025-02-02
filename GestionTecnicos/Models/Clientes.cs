using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionTecnicos.Models;


public class Clientes
{
    [Key]
    public int ClienteId { get; set; }
    [Required]
    public int TecnicoId { get; set; }
    
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El campo Nombres es requerido.")]
    public string Nombres { get; set; } = null!;

    [Required(ErrorMessage = "El campo Direccion es requerido.")]
    public string Direccion { get; set; } = null!;
    
    [Required(ErrorMessage = "El campo Rnc es requerido.")]
    [StringLength(11,MinimumLength = 9)]
    [RegularExpression(@"^\d+$", ErrorMessage = "El RNC solo puede contener números.")]
    public string Rnc { get; set; } = null!;
    
    [Range(1, double.MaxValue, ErrorMessage = "El monto no puede ser menor a 1")]
    public float LimiteCredito { get; set; }

    [ForeignKey("TecnicoId")] 
    public Tecnicos Tecnico { get; set; } = null!;


}