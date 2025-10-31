using System.ComponentModel.DataAnnotations;
using Proyecto.Attributes;

namespace Proyecto.DTOS;

public class VehiculoDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El número de motor es obligatorio")]
    [StringLength(20, ErrorMessage = "El número de motor no puede exceder los 20 caracteres")]
    public string NoMotor { get; set; } = string.Empty;

    [Required(ErrorMessage = "El VIN es obligatorio")]
    [StringLength(20, ErrorMessage = "El VIN no puede exceder los 20 caracteres")]
    public string Vin { get; set; } = string.Empty;

    [Required(ErrorMessage = "La cilindrada es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "La cilindrada debe ser mayor a 0")]
    public int Cilindrada { get; set; }

    [Required(ErrorMessage = "La placa es obligatoria")]
    [StringLength(10, ErrorMessage = "La placa no puede exceder los 10 caracteres")]
    public string Placa { get; set; } = string.Empty;

    [Required(ErrorMessage = "El modelo es obligatorio")]
    [YearRange(1980)]
    public int Modelo { get; set; }

    [Required(ErrorMessage = "El ID del equipo es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del equipo debe ser mayor a 0")]
    public int IdEquipo { get; set; }
}