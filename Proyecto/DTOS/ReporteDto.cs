using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class ReporteDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "La observación es obligatoria")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "La observación debe tener entre 10 y 1000 caracteres")]
    public string? Observacion { get; set; }

    [Required(ErrorMessage = "Debe seleccionar una causa")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una causa válida")]
    public int IdCausa { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un equipo")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un equipo válido")]
    public int IdEquipo { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un empleado")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un empleado válido")]
    public int IdEmpleado { get; set; }

    public string? NombreEmpleado { get; set; }
    public string? NombreEquipo { get; set; }
    public string? IdentificadorEquipo { get; set; }
    public string? NombreCausa { get; set; }
}