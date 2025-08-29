using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class ElectronicoDto
{
    public int? Id { get; set; }

    [StringLength(20, ErrorMessage = "El IMEI no puede exceder los 20 caracteres")]
    public string? Imei { get; set; }

    [StringLength(20, ErrorMessage = "El sistema operativo no puede exceder los 20 caracteres")]
    public string? SistemaOperativo { get; set; }

    [RegularExpression(@"^(bluetooth|wifi|gsm|nfc|bluetooth_wifi|bluetooth_gsm|bluetooth_nfc|wifi_gsm|wifi_nfc|gsm_nfc|bluetooth_wifi_gsm|bluetooth_wifi_nfc|bluetooth_gsm_nfc|wifi_gsm_nfc|bluetooth_wifi_gsm_nfc|ninguno)$", ErrorMessage = "La conectividad debe ser un valor válido")]
    public string? Conectividad { get; set; }

    [RegularExpression(@"^(starlink|claro|tigo|comnet|verasat|telecom|ninguno)$", ErrorMessage = "El operador debe ser un valor válido")]
    public string? Operador { get; set; }

    [Required(ErrorMessage = "El ID del equipo es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del equipo debe ser mayor a 0")]
    public int IdEquipo { get; set; }
}