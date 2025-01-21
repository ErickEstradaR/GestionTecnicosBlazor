using System.ComponentModel.DataAnnotations.Schema;

namespace GestionTecnicos.Models;
using System.ComponentModel.DataAnnotations;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "El campo Nombres es requerido.")]
    public string Nombres { get; set; } = null!;

    [Required(ErrorMessage = "El campo Direccion es requerido.")]
    public string Direccion { get; set; } = null!;
    
    [Required(ErrorMessage = "El campo Rnc es requerido.")]
    public string Rnc { get; set; } = null!;
    
    [Range(1, double.MaxValue, ErrorMessage = "El monto no puede ser menor a 1")]
    public float LimiteCredito { get; set; }

    [ForeignKey("TecnicoId")] 
    public Tecnicos Tecnico { get; set; } = null!;


}