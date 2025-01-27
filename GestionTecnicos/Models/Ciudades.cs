
using System.ComponentModel.DataAnnotations;

namespace GestionTecnicos.Models;


public class Ciudades
{
    [Key]
    public string CiudadId { get; set; }
    [Required(ErrorMessage=("Este campo es requerido"))]
    public string NombreCiudad { get; set; }
}