using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class HerramientaDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El material es obligatorio")]
    [StringLength(50, ErrorMessage = "El material no puede exceder los 50 caracteres")]
    public string Material { get; set; } = string.Empty;

    [Required(ErrorMessage = "El ID del equipo es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del equipo debe ser mayor a 0")]
    public int IdEquipo { get; set; }
}