
using System.ComponentModel.DataAnnotations;

namespace Williams_AP1_P1.Models;

public class Autor
{
    [Key]
    public int IdAutor { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La Nacionalidad es obligatoria.")]
    public string Nacionalidad { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "El sueldo es obligatorio.")]
    public decimal Sueldo { get; set; }
}

