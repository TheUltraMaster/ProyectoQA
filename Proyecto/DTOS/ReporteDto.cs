using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class ReporteDto
{
    public int? Id { get; set; }

    [StringLength(1000, ErrorMessage = "La observación no puede exceder los 1000 caracteres")]
    public string? Observacion { get; set; }

    [Required(ErrorMessage = "El ID de la causa es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID de la causa debe ser mayor a 0")]
    public int IdCausa { get; set; }

    [Required(ErrorMessage = "El ID del equipo es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del equipo debe ser mayor a 0")]
    public int IdEquipo { get; set; }

    [Required(ErrorMessage = "El ID del empleado es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del empleado debe ser mayor a 0")]
    public int IdEmpleado { get; set; }
}