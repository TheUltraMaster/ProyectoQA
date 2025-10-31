using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class MobiliarioDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El material es obligatorio")]
    [StringLength(50, ErrorMessage = "El material no puede exceder los 50 caracteres")]
    public string Material { get; set; } = string.Empty;

    [Required(ErrorMessage = "La altura es obligatoria")]
    [Range(0.01, 999.99, ErrorMessage = "La altura debe estar entre 0.01 y 999.99")]
    public float Altura { get; set; }

    [Required(ErrorMessage = "El ancho es obligatorio")]
    [Range(0.01, 999.99, ErrorMessage = "El ancho debe estar entre 0.01 y 999.99")]
    public float Ancho { get; set; }

    [Required(ErrorMessage = "La profundidad es obligatoria")]
    [Range(0.01, 999.99, ErrorMessage = "La profundidad debe estar entre 0.01 y 999.99")]
    public float Profundidad { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad de piezas debe ser mayor a 0")]
    public int? CantidadPiezas { get; set; }

    [Required(ErrorMessage = "El ID del equipo es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del equipo debe ser mayor a 0")]
    public int IdEquipo { get; set; }
}