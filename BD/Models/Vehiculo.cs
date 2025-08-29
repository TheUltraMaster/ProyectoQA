using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD.Models;

[Table("vehiculo")]
[Index("IdEquipo", Name = "id_equipo")]
public partial class Vehiculo
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("no_motor")]
    [StringLength(20)]
    public string NoMotor { get; set; } = null!;

    [Column("vin")]
    [StringLength(20)]
    public string Vin { get; set; } = null!;

    [Column("cilindrada", TypeName = "int(11)")]
    public int Cilindrada { get; set; }

    [Column("placa")]
    [StringLength(10)]
    public string Placa { get; set; } = null!;

    [Column("modelo", TypeName = "int(11)")]
    public int Modelo { get; set; }

    [Column("id_equipo", TypeName = "int(11)")]
    public int IdEquipo { get; set; }

    [ForeignKey("IdEquipo")]
    [InverseProperty("Vehiculos")]
    public virtual Equipo IdEquipoNavigation { get; set; } = null!;
}
