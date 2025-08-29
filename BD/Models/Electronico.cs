using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD.Models;

[Table("electronico")]
[Index("IdEquipo", Name = "id_equipo")]
public partial class Electronico
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("imei")]
    [StringLength(20)]
    public string? Imei { get; set; }

    [Column("sistema_operativo")]
    [StringLength(20)]
    public string? SistemaOperativo { get; set; }

    [Column("conectividad", TypeName = "enum('bluetooth','wifi','gsm','nfc','bluetooth_wifi','bluetooth_gsm','bluetooth_nfc','wifi_gsm','wifi_nfc','gsm_nfc','bluetooth_wifi_gsm','bluetooth_wifi_nfc','bluetooth_gsm_nfc','wifi_gsm_nfc','bluetooth_wifi_gsm_nfc','ninguno')")]
    public string? Conectividad { get; set; }

    [Column("operador", TypeName = "enum('starlink','claro','tigo','comnet','verasat','telecom','ninguno')")]
    public string? Operador { get; set; }

    [Column("id_equipo", TypeName = "int(11)")]
    public int IdEquipo { get; set; }

    [ForeignKey("IdEquipo")]
    [InverseProperty("Electronicos")]
    public virtual Equipo IdEquipoNavigation { get; set; } = null!;
}
