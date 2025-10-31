using BD.Data;
using BD.Models;
using Microsoft.EntityFrameworkCore;
using Proyecto.Services;

namespace Proyecto.Services;

public class BitacoraEquipoService : IBitacoraEquipoService
{
    private readonly ProyectoContext _context;

    public BitacoraEquipoService(ProyectoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BitacoraEquipo>> GetAllAsync()
    {
        return await _context.BitacoraEquipos
            .Include(b => b.IdEmpleadoNavigation)
            .Include(b => b.IdEquipoNavigation)
            .OrderByDescending(b => b.FechaCommit)
            .ToListAsync();
    }

    public async Task<IEnumerable<BitacoraEquipo>> GetFilteredAsync(string? empleadoNombre = null, string? equipoIdentificador = null, string? equipoNombre = null)
    {
        var query = _context.BitacoraEquipos
            .Include(b => b.IdEmpleadoNavigation)
            .Include(b => b.IdEquipoNavigation)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(empleadoNombre))
        {
            query = query.Where(b => b.IdEmpleadoNavigation != null && 
                (b.IdEmpleadoNavigation.PrimerNombre.Contains(empleadoNombre) ||
                 b.IdEmpleadoNavigation.PrimerApellido.Contains(empleadoNombre) ||
                 (b.IdEmpleadoNavigation.SegundoNombre != null && b.IdEmpleadoNavigation.SegundoNombre.Contains(empleadoNombre)) ||
                 (b.IdEmpleadoNavigation.SegundoApellido != null && b.IdEmpleadoNavigation.SegundoApellido.Contains(empleadoNombre))));
        }

        if (!string.IsNullOrWhiteSpace(equipoIdentificador))
        {
            query = query.Where(b => b.IdEquipoNavigation.Identificador != null && 
                b.IdEquipoNavigation.Identificador.Contains(equipoIdentificador));
        }

        if (!string.IsNullOrWhiteSpace(equipoNombre))
        {
            query = query.Where(b => b.IdEquipoNavigation.Nombre.Contains(equipoNombre));
        }

        return await query.OrderByDescending(b => b.FechaCommit).ToListAsync();
    }

    public async Task<BitacoraEquipo?> GetByIdAsync(int id)
    {
        return await _context.BitacoraEquipos
            .Include(b => b.IdEmpleadoNavigation)
            .Include(b => b.IdEquipoNavigation)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<BitacoraEquipo>> GetByUserAreasAsync(int userId, int? areaId = null)
    {
        var userAreas = await _context.Areas
            .Where(a => a.IdUsuario == userId)
            .Select(a => a.Id)
            .ToListAsync();

        if (userAreas.Count == 0)
        {
            return new List<BitacoraEquipo>();
        }

        var query = _context.BitacoraEquipos
            .Include(b => b.IdEmpleadoNavigation)
                .ThenInclude(e => e!.IdAreaNavigation)
            .Include(b => b.IdEquipoNavigation)
            .Where(b => b.IdEmpleadoNavigation != null && 
                       b.IdEmpleadoNavigation.IdArea != null &&
                       userAreas.Contains(b.IdEmpleadoNavigation.IdArea.Value));

        if (areaId.HasValue)
        {
            query = query.Where(b => b.IdEmpleadoNavigation!.IdArea == areaId.Value);
        }

        return await query.OrderByDescending(b => b.FechaCommit).ToListAsync();
    }

    public async Task<IEnumerable<Area>> GetUserAreasAsync(int userId)
    {
        return await _context.Areas
            .Where(a => a.IdUsuario == userId)
            .OrderBy(a => a.Nombre)
            .ToListAsync();
    }

    public async Task<(List<BitacoraEquipo> BitacoraEquipos, int TotalCount)> GetBitacoraEquiposPagedAsync(int page, int pageSize, string? empleadoNombre = null, string? equipoNombre = null, string? equipoIdentificador = null, bool? asignado = null, DateTime? fechaInicio = null, DateTime? fechaFin = null)
    {
        var query = _context.BitacoraEquipos
            .Include(b => b.IdEmpleadoNavigation)
            .Include(b => b.IdEquipoNavigation)
            .AsQueryable();

        // Filter by employee name
        if (!string.IsNullOrWhiteSpace(empleadoNombre))
        {
            query = query.Where(b => b.IdEmpleadoNavigation != null && 
                (b.IdEmpleadoNavigation.PrimerNombre.Contains(empleadoNombre) ||
                 b.IdEmpleadoNavigation.PrimerApellido.Contains(empleadoNombre) ||
                 (b.IdEmpleadoNavigation.SegundoNombre != null && b.IdEmpleadoNavigation.SegundoNombre.Contains(empleadoNombre)) ||
                 (b.IdEmpleadoNavigation.SegundoApellido != null && b.IdEmpleadoNavigation.SegundoApellido.Contains(empleadoNombre)) ||
                 (b.IdEmpleadoNavigation.PrimerNombre + " " + b.IdEmpleadoNavigation.PrimerApellido).Contains(empleadoNombre)));
        }

        // Filter by equipment name
        if (!string.IsNullOrWhiteSpace(equipoNombre))
        {
            query = query.Where(b => b.IdEquipoNavigation.Nombre.Contains(equipoNombre));
        }

        // Filter by equipment identifier
        if (!string.IsNullOrWhiteSpace(equipoIdentificador))
        {
            query = query.Where(b => b.IdEquipoNavigation.Identificador != null && 
                b.IdEquipoNavigation.Identificador.Contains(equipoIdentificador));
        }

        // Filter by assignment status (asignado = true if IdEmpleado is not null)
        if (asignado.HasValue)
        {
            if (asignado.Value)
            {
                query = query.Where(b => b.IdEmpleado != null);
            }
            else
            {
                query = query.Where(b => b.IdEmpleado == null);
            }
        }

        // Filter by date range
        if (fechaInicio.HasValue)
        {
            query = query.Where(b => b.FechaCommit >= fechaInicio.Value);
        }

        if (fechaFin.HasValue)
        {
            query = query.Where(b => b.FechaCommit <= fechaFin.Value);
        }

        var totalCount = await query.CountAsync();

        var bitacoraEquipos = await query
            .OrderByDescending(b => b.FechaCommit)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (bitacoraEquipos, totalCount);
    }

    public async Task<IEnumerable<Empleado>> GetEmpleadosForFilterAsync(int page = 1, int pageSize = 50, string? search = null)
    {
        var query = _context.Empleados.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(e => 
                (e.PrimerNombre + " " + e.SegundoNombre + " " + e.PrimerApellido + " " + e.SegundoApellido).Contains(search) ||
                e.PrimerNombre.Contains(search) ||
                e.PrimerApellido.Contains(search));
        }

        return await query
            .OrderBy(e => e.PrimerNombre)
            .ThenBy(e => e.PrimerApellido)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Equipo>> GetEquiposForFilterAsync(int page = 1, int pageSize = 50, string? search = null)
    {
        var query = _context.Equipos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(e => 
                e.Nombre.Contains(search) ||
                (e.Identificador != null && e.Identificador.Contains(search)));
        }

        return await query
            .OrderBy(e => e.Nombre)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

}