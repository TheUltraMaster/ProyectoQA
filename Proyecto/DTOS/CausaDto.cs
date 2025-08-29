using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class CausaDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El nombre de la causa es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-_]+$", ErrorMessage = "El nombre solo puede contener letras, espacios, guiones y guiones bajos")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(300, ErrorMessage = "La descripción no puede exceder los 300 caracteres")]
    public string? Descripcion { get; set; }
}