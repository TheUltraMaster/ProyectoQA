using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class MarcaDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El nombre de la marca es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[A-ZÁÉÍÓÚÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras mayúsculas y espacios")]
    public string Nombre { get; set; } = string.Empty;
}