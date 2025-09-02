using System;
using System.Collections.Generic;
using BD.Models;
using Microsoft.EntityFrameworkCore;

namespace BD.Data;

public partial class ProyectoContext : DbContext
{
    public ProyectoContext()
    {
    }

    public ProyectoContext(DbContextOptions<ProyectoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<BitacoraEquipo> BitacoraEquipos { get; set; }

    public virtual DbSet<Causa> Causas { get; set; }

    public virtual DbSet<Electronico> Electronicos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Herramientum> Herramienta { get; set; }

    public virtual DbSet<Imagen> Imagens { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Mobiliario> Mobiliarios { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=proyecto_QA;Uid=root;Pwd=Judith0709;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Areas)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("area_ibfk_1");
        });

        modelBuilder.Entity<BitacoraEquipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaCommit).HasDefaultValueSql("'current_timestamp()'");
            entity.Property(e => e.IdEmpleado).HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.BitacoraEquipos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("bitacoraEquipo_ibfk_1");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.BitacoraEquipos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("bitacoraEquipo_ibfk_2");
        });

        modelBuilder.Entity<Causa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Descripcion).HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Electronico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Conectividad).HasDefaultValueSql("'''ninguno'''");
            entity.Property(e => e.Imei).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Operador).HasDefaultValueSql("'''ninguno'''");
            entity.Property(e => e.SistemaOperativo).HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Electronicos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("electronico_ibfk_1");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'''activo'''");
            entity.Property(e => e.IdArea).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.IdUsuario).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.SegundoApellido).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.SegundoNombre).HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Empleados)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("empleado_ibfk_2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Empleado)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("empleado_ibfk_1");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'''activo'''");
            entity.Property(e => e.Extras).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.FechaCommit).HasDefaultValueSql("'current_timestamp()'");
            entity.Property(e => e.IdEmpleado).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Identificador).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.TipoAlimentacion).HasDefaultValueSql("'''ninguna'''");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Equipos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("equipo_ibfk_2");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Equipos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("equipo_ibfk_1");
        });

        modelBuilder.Entity<Herramientum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Herramienta)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("herramienta_ibfk_1");
        });

        modelBuilder.Entity<Imagen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.IdReporteNavigation).WithMany(p => p.Imagens)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("imagen_ibfk_1");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Mobiliario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CantidadPiezas).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Mobiliarios)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("mobiliario_ibfk_1");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaCommit).HasDefaultValueSql("'current_timestamp()'");
            entity.Property(e => e.Observacion).HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.IdCausaNavigation).WithMany(p => p.Reportes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("reporte_ibfk_1");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Reportes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("reporte_ibfk_3");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Reportes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("reporte_ibfk_2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaCommit).HasDefaultValueSql("'current_timestamp()'");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Vehiculos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("vehiculo_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
