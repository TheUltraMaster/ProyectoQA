using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD.Models;

[Table("imagen")]
[Index("IdReporte", Name = "id_reporte")]
public partial class Imagen
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("url", TypeName = "text")]
    public string Url { get; set; } = null!;

    [Column("id_reporte", TypeName = "int(11)")]
    public int IdReporte { get; set; }

    [ForeignKey("IdReporte")]
    [InverseProperty("Imagens")]
    public virtual Reporte IdReporteNavigation { get; set; } = null!;
}
