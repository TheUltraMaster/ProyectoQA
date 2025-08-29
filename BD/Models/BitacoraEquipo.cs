using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD.Models;

[Table("bitacoraEquipo")]
[Index("IdEmpleado", Name = "idx_bitacora_empleado")]
[Index("IdEquipo", Name = "idx_bitacora_equipo")]
public partial class BitacoraEquipo
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("id_empleado", TypeName = "int(11)")]
    public int? IdEmpleado { get; set; }

    [Column("id_equipo", TypeName = "int(11)")]
    public int IdEquipo { get; set; }

    [Column("fecha_commit", TypeName = "datetime")]
    public DateTime? FechaCommit { get; set; }

    [ForeignKey("IdEmpleado")]
    [InverseProperty("BitacoraEquipos")]
    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    [ForeignKey("IdEquipo")]
    [InverseProperty("BitacoraEquipos")]
    public virtual Equipo IdEquipoNavigation { get; set; } = null!;
}
