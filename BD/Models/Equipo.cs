using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD.Models;

[Table("equipo")]
[Index("IdEmpleado", Name = "idx_equipo_empleado")]
[Index("IdMarca", Name = "idx_equipo_marca")]
[Index("Tipo", Name = "idx_equipo_tipo")]
public partial class Equipo
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("identificador")]
    [StringLength(20)]
    public string? Identificador { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [Column("id_marca", TypeName = "int(11)")]
    public int IdMarca { get; set; }

    [Column("color")]
    [StringLength(50)]
    public string Color { get; set; } = null!;

    [Column("valor")]
    [Precision(12)]
    public decimal Valor { get; set; }

    [Column("serie")]
    [StringLength(50)]
    public string Serie { get; set; } = null!;

    [Column("extras", TypeName = "text")]
    public string? Extras { get; set; }

    [Column("tipo_alimentacion", TypeName = "enum('110v','220v','diesel','regular','super','bateria','ninguna')")]
    public string? TipoAlimentacion { get; set; }

    [Column("id_empleado", TypeName = "int(11)")]
    public int? IdEmpleado { get; set; }

    [Column("estado", TypeName = "enum('activo','inactivo','mantenimiento','suspendido')")]
    public string? Estado { get; set; }

    [Column("fecha_commit", TypeName = "datetime")]
    public DateTime? FechaCommit { get; set; }

    [Column("tipo", TypeName = "enum('mobiliario','vehiculo','electronico','herramienta')")]
    public string Tipo { get; set; } = null!;

    [InverseProperty("IdEquipoNavigation")]
    public virtual ICollection<BitacoraEquipo> BitacoraEquipos { get; set; } = new List<BitacoraEquipo>();

    [InverseProperty("IdEquipoNavigation")]
    public virtual ICollection<Electronico> Electronicos { get; set; } = new List<Electronico>();

    [InverseProperty("IdEquipoNavigation")]
    public virtual ICollection<Herramientum> Herramienta { get; set; } = new List<Herramientum>();

    [ForeignKey("IdEmpleado")]
    [InverseProperty("Equipos")]
    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    [ForeignKey("IdMarca")]
    [InverseProperty("Equipos")]
    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    [InverseProperty("IdEquipoNavigation")]
    public virtual ICollection<Mobiliario> Mobiliarios { get; set; } = new List<Mobiliario>();

    [InverseProperty("IdEquipoNavigation")]
    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();

    [InverseProperty("IdEquipoNavigation")]
    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
