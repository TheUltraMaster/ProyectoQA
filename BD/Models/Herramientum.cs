using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD.Models;

[Table("herramienta")]
[Index("IdEquipo", Name = "id_equipo")]
public partial class Herramientum
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("material")]
    [StringLength(50)]
    public string Material { get; set; } = null!;

    [Column("id_equipo", TypeName = "int(11)")]
    public int IdEquipo { get; set; }

    [ForeignKey("IdEquipo")]
    [InverseProperty("Herramienta")]
    public virtual Equipo IdEquipoNavigation { get; set; } = null!;
}
