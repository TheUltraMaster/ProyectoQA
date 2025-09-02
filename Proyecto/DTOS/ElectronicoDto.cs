using System.ComponentModel.DataAnnotations;
using BD.Models;

namespace Proyecto.DTOS;

public class ElectronicoDto
{
    public int? Id { get; set; }
    
    [StringLength(20, ErrorMessage = "El IMEI no puede exceder los 20 caracteres")]
    [RegularExpression(@"^[0-9]{15}$", ErrorMessage = "El IMEI debe tener exactamente 15 dígitos")]
    public string? Imei { get; set; }
    
    [StringLength(20, ErrorMessage = "El sistema operativo no puede exceder los 20 caracteres")]
    public string? SistemaOperativo { get; set; }
    
    public Conectividad? Conectividad { get; set; }
    
    public Operador? Operador { get; set; }
    
    [Required(ErrorMessage = "El ID del equipo es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del equipo debe ser mayor a 0")]
    public int IdEquipo { get; set; }
}