using System.ComponentModel.DataAnnotations;
using BD.Models;

namespace Proyecto.DTOS;

public class EquipoDto
{
    public int? Id { get; set; }

    [StringLength(20, ErrorMessage = "El identificador no puede exceder los 20 caracteres")]
    [RegularExpression(@"^[a-zA-Z0-9\-_]*$", ErrorMessage = "El identificador solo puede contener letras, números, guiones y guiones bajos")]
    public string? Identificador { get; set; }

    [Required(ErrorMessage = "El nombre del equipo es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s\-_]+$", ErrorMessage = "El nombre solo puede contener letras, números, espacios, guiones y guiones bajos")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El ID de la marca es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID de la marca debe ser mayor a 0")]
    public int IdMarca { get; set; }

    [Required(ErrorMessage = "El color es obligatorio")]
    [StringLength(50, ErrorMessage = "El color no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El color solo puede contener letras y espacios")]
    public string Color { get; set; } = string.Empty;

    [Required(ErrorMessage = "El valor es obligatorio")]
    [Range(0.01, 999999999999.99, ErrorMessage = "El valor debe estar entre 0.01 y 999,999,999,999.99")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "El número de serie es obligatorio")]
    [StringLength(50, ErrorMessage = "El número de serie no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[a-zA-Z0-9\-_]+$", ErrorMessage = "El número de serie solo puede contener letras, números, guiones y guiones bajos")]
    public string Serie { get; set; } = string.Empty;

    public string? Extras { get; set; }

    public TipoAlimentacion? TipoAlimentacion { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El ID del empleado debe ser mayor a 0")]
    public int? IdEmpleado { get; set; }

    public EstadoEquipo Estado { get; set; } = EstadoEquipo.Activo;

    [Required(ErrorMessage = "El tipo de equipo es obligatorio")]
    public TipoEquipo Tipo { get; set; }
    
    public string? EmpleadoNombre { get; set; }
    
    public string? MarcaNombre { get; set; }
}