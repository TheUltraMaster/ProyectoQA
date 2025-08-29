using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD.Models;

[Table("reporte")]
[Index("IdCausa", Name = "id_causa")]
[Index("IdEmpleado", Name = "idx_reporte_empleado")]
[Index("IdEquipo", Name = "idx_reporte_equipo")]
public partial class Reporte
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("observacion", TypeName = "text")]
    public string? Observacion { get; set; }

    [Column("id_causa", TypeName = "int(11)")]
    public int IdCausa { get; set; }

    [Column("id_equipo", TypeName = "int(11)")]
    public int IdEquipo { get; set; }

    [Column("id_empleado", TypeName = "int(11)")]
    public int IdEmpleado { get; set; }

    [Column("fecha_commit", TypeName = "datetime")]
    public DateTime? FechaCommit { get; set; }

    [ForeignKey("IdCausa")]
    [InverseProperty("Reportes")]
    public virtual Causa IdCausaNavigation { get; set; } = null!;

    [ForeignKey("IdEmpleado")]
    [InverseProperty("Reportes")]
    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    [ForeignKey("IdEquipo")]
    [InverseProperty("Reportes")]
    public virtual Equipo IdEquipoNavigation { get; set; } = null!;

    [InverseProperty("IdReporteNavigation")]
    public virtual ICollection<Imagen> Imagens { get; set; } = new List<Imagen>();
}
