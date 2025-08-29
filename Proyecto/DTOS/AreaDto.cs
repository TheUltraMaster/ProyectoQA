using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class AreaDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El nombre del área es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El ID del usuario es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario debe ser mayor a 0")]
    public int IdUsuario { get; set; }
}