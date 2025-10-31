using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD.Models;

[Table("mobiliario")]
[Index("IdEquipo", Name = "id_equipo")]
public partial class Mobiliario
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("material")]
    [StringLength(50)]
    public string Material { get; set; } = null!;

    [Column("altura", TypeName = "float(8,2)")]
    public float Altura { get; set; }

    [Column("ancho", TypeName = "float(8,2)")]
    public float Ancho { get; set; }

    [Column("profundidad", TypeName = "float(8,2)")]
    public float Profundidad { get; set; }

    [Column("cantidad_piezas", TypeName = "int(11)")]
    public int? CantidadPiezas { get; set; }

    [Column("id_equipo", TypeName = "int(11)")]
    public int IdEquipo { get; set; }

    [ForeignKey("IdEquipo")]
    [InverseProperty("Mobiliarios")]
    public virtual Equipo IdEquipoNavigation { get; set; } = null!;
}
