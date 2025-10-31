using BD.Models;
using Bycript;
using Microsoft.EntityFrameworkCore;
using BD.Data;

namespace Proyecto.Data;

public static class SeedData
{
    public static async Task SeedUsuarios(ProyectoContext context, IBCryptService bcryptService)
    {
        if (await context.Usuarios.AnyAsync())
        {
            return; // Ya existen usuarios, no agregar datos semilla
        }

        var usuarios = new List<Usuario>
        {
            new Usuario
            {
                Usuario1 = "admin@empresa.com",
                Password = bcryptService.HashText("Admin123456"),
                IsAdmin = true,
                Activo = 1, // bit(1) -> ulong
                FechaCommit = DateTime.Now
            },
            new Usuario
            {
                Usuario1 = "usuario1@empresa.com",
                Password = bcryptService.HashText("Usuario123456"),
                IsAdmin = false,
                Activo = 1,
                FechaCommit = DateTime.Now
            },
            new Usuario
            {
                Usuario1 = "supervisor@empresa.com",
                Password = bcryptService.HashText("Supervisor123456"),
                IsAdmin = false,
                Activo = 0,
                FechaCommit = DateTime.Now
            },
            new Usuario
            {
                Usuario1 = "gerente@empresa.com",
                Password = bcryptService.HashText("Gerente123456"),
                IsAdmin = true,
                Activo = 0,
                FechaCommit = DateTime.Now
            },
            new Usuario
            {
                Usuario1 = "operador@empresa.com",
                Password = bcryptService.HashText("Operador123456"),
                IsAdmin = false,
                Activo = 0,
                FechaCommit = DateTime.Now
            }
        };

        context.Usuarios.AddRange(usuarios);
        await context.SaveChangesAsync();
    }
}