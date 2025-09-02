using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD.Models;

[Table("empleado")]
[Index("IdUsuario", Name = "id_usuario", IsUnique = true)]
[Index("IdArea", Name = "idx_empleado_area")]
[Index("IdUsuario", Name = "idx_empleado_usuario")]
public partial class Empleado
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("primer_nombre")]
    [StringLength(50)]
    public string PrimerNombre { get; set; } = null!;

    [Column("segundo_nombre")]
    [StringLength(50)]
    public string? SegundoNombre { get; set; }

    [Column("primer_apellido")]
    [StringLength(50)]
    public string PrimerApellido { get; set; } = null!;

    [Column("segundo_apellido")]
    [StringLength(50)]
    public string? SegundoApellido { get; set; }

    [Column("id_usuario", TypeName = "int(11)")]
    public int? IdUsuario { get; set; }

    [Column("estado", TypeName = "enum('activo','inactivo','vacaciones')")]
    public string? Estado { get; set; }

    [Column("id_area", TypeName = "int(11)")]
    public int? IdArea { get; set; }

    [InverseProperty("IdEmpleadoNavigation")]
    public virtual ICollection<BitacoraEquipo> BitacoraEquipos { get; set; } = new List<BitacoraEquipo>();

    [InverseProperty("IdEmpleadoNavigation")]
    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    [ForeignKey("IdArea")]
    [InverseProperty("Empleados")]
    public virtual Area? IdAreaNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Empleado")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }

    [InverseProperty("IdEmpleadoNavigation")]
    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();
}
