using System.ComponentModel.DataAnnotations;

namespace Williams_AP1_P1.Models;

public class Autor
{
    [Key]
    public int IdAutor { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La nacionalidad es obligatoria.")]
    [StringLength(60, ErrorMessage = "La nacionalidad no puede superar los 60 caracteres.")]
    public string Nacionalidad { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaNacimiento { get; set; }


    [Range(0, 1000000000, ErrorMessage = "El sueldo debe ser igual o mayor que cero.")]
    public decimal Sueldo { get; set; }
}
