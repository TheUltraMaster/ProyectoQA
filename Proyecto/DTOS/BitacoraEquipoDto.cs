using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class BitacoraEquipoDto
{
    public int? Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El ID del empleado debe ser mayor a 0")]
    public int? IdEmpleado { get; set; }

    [Required(ErrorMessage = "El ID del equipo es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del equipo debe ser mayor a 0")]
    public int IdEquipo { get; set; }
}