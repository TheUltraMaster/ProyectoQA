using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD.Models;

[Table("area")]
[Index("IdUsuario", Name = "id_usuario")]
public partial class Area
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [Column("id_usuario", TypeName = "int(11)")]
    public int IdUsuario { get; set; }

    [InverseProperty("IdAreaNavigation")]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("Areas")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
