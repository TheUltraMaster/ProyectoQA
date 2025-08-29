using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class ImagenDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "La URL es obligatoria")]
    public string Url { get; set; } = string.Empty;

    [Required(ErrorMessage = "El ID del reporte es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del reporte debe ser mayor a 0")]
    public int IdReporte { get; set; }
}