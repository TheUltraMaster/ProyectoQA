using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class CausaDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[A-ZÁÉÍÓÚÑ\s]+$", ErrorMessage = "El nombre debe estar en mayúsculas y solo puede contener letras y espacios")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(300, ErrorMessage = "La descripción no puede exceder los 300 caracteres")]
    public string? Descripcion { get; set; }
}