using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class MarcaDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El nombre de la marca es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s\-_&\.]+$", ErrorMessage = "El nombre solo puede contener letras, números, espacios y algunos caracteres especiales")]
    public string Nombre { get; set; } = string.Empty;
}