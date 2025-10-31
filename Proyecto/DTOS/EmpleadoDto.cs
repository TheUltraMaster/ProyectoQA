using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class EmpleadoDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El primer nombre es obligatorio")]
    [StringLength(50, ErrorMessage = "El primer nombre no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$", ErrorMessage = "El primer nombre solo puede contener letras")]
    public string PrimerNombre { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "El segundo nombre no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$", ErrorMessage = "El segundo nombre solo puede contener letras")]
    public string SegundoNombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El primer apellido es obligatorio")]
    [StringLength(50, ErrorMessage = "El primer apellido no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$", ErrorMessage = "El primer apellido solo puede contener letras")]
    public string PrimerApellido { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "El segundo apellido no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$", ErrorMessage = "El segundo apellido solo puede contener letras")]
    public string SegundoApellido { get; set; } = string.Empty;

    [RegularExpression(@"^(activo|inactivo|vacaciones)$", ErrorMessage = "El estado debe ser: activo, inactivo o vacaciones")]
    public string? Estado { get; set; } = "activo";

    [Range(1, int.MaxValue, ErrorMessage = "El ID del área debe ser mayor a 0")]
    public int? IdArea { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario debe ser mayor a 0")]
    public int? IdUsuario { get; set; }
}