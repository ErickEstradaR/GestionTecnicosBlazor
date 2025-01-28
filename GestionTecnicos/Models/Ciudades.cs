using System.ComponentModel.DataAnnotations;

namespace GestionTecnicos.Models;


public class Ciudades
{
    [Key]
    public int CiudadId { get; set; }
    [Required(ErrorMessage=("Este campo es requerido"))]
    public string NombreCiudad { get; set; }
}