using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD.Models;

[Table("usuario")]
[Index("Usuario1", Name = "usuario", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("usuario")]
    [StringLength(50)]
    public string Usuario1 { get; set; } = null!;

    [Column("is_admin")]
    public bool IsAdmin { get; set; }

    [Column("password")]
    [StringLength(100)]
    public string Password { get; set; } = null!;

    [Column("fecha_commit", TypeName = "datetime")]
    public DateTime? FechaCommit { get; set; }

    [Column("activo", TypeName = "bit(1)")]
    public ulong Activo { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual Empleado? Empleado { get; set; }
}
