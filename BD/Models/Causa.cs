using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD.Models;

[Table("causa")]
public partial class Causa
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(300)]
    public string? Descripcion { get; set; }

    [InverseProperty("IdCausaNavigation")]
    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();
}
