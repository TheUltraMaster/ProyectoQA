using BD.Data;
using BD.Models;
using Microsoft.EntityFrameworkCore;
using Proyecto.DTOS;
using Proyecto.Mapper;
using System.Globalization;
using System.Text;

namespace Proyecto.Services;

public class AreaService : IAreaService
{
    private readonly ProyectoContext _context;
    private readonly Map _mapper;

    public AreaService(ProyectoContext context, Map mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Area>> GetAllAreasAsync()
    {
        return await _context.Areas
            .Include(a => a.IdUsuarioNavigation)
            .Include(a => a.Empleados)
            .OrderBy(a => a.Nombre)
            .ToListAsync();
    }

    public async Task<(List<Area> Areas, int TotalCount)> GetAreasPagedAsync(int page, int pageSize, string? search = null)
    {
        var query = _context.Areas
            .Include(a => a.IdUsuarioNavigation)
            .Include(a => a.Empleados)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(a => a.Nombre.Contains(search));
        }

        var totalCount = await query.CountAsync();

        var areas = await query
            .OrderBy(a => a.Nombre)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (areas, totalCount);
    }

    public async Task<Area?> GetAreaByIdAsync(int id)
    {
        return await _context.Areas
            .Include(a => a.IdUsuarioNavigation)
            .Include(a => a.Empleados)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Area> CreateAreaAsync(AreaDto areaDto)
    {
        // Normalize the name: uppercase and remove accents
        areaDto.Nombre = NormalizeAreaName(areaDto.Nombre);
        
        // Check if name already exists
        if (await AreaNameExistsAsync(areaDto.Nombre))
        {
            throw new InvalidOperationException("Ya existe un área con ese nombre");
        }

        var area = _mapper.toEntity(areaDto);

        _context.Areas.Add(area);
        await _context.SaveChangesAsync();
        
        return await GetAreaByIdAsync(area.Id) ?? area;
    }

    public async Task<Area?> UpdateAreaAsync(int id, AreaDto areaDto)
    {
        var area = await _context.Areas.FindAsync(id);
        if (area == null)
            return null;

        // Normalize the name: uppercase and remove accents
        areaDto.Nombre = NormalizeAreaName(areaDto.Nombre);
        
        // Check if name already exists (excluding current area)
        if (await AreaNameExistsAsync(areaDto.Nombre, id))
        {
            throw new InvalidOperationException("Ya existe un área con ese nombre");
        }

        var updatedArea = _mapper.toEntity(areaDto);
        area.Nombre = updatedArea.Nombre;
        area.IdUsuario = updatedArea.IdUsuario;

        await _context.SaveChangesAsync();
        return await GetAreaByIdAsync(id);
    }

    public async Task<bool> DeleteAreaAsync(int id)
    {
        var area = await _context.Areas.FindAsync(id);
        if (area == null)
            return false;

        var hasEmpleados = await _context.Empleados.AnyAsync(e => e.IdArea == id);
        if (hasEmpleados)
            return false;

        _context.Areas.Remove(area);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AreaExistsAsync(int id)
    {
        return await _context.Areas.AnyAsync(a => a.Id == id);
    }

    public async Task<List<Usuario>> GetUsuariosAsync()
    {
        return await _context.Usuarios
            .OrderBy(u => u.Usuario1)
            .ToListAsync();
    }

    public async Task<(List<Usuario> usuarios, int totalCount)> GetUsuariosPaginadosAsync(int page, int pageSize, string? search = null)
    {
        var query = _context.Usuarios.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(u => u.Usuario1.Contains(search));
        }

        var totalCount = await query.CountAsync();

        var usuarios = await query
            .OrderBy(u => u.Usuario1)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (usuarios, totalCount);
    }

    public async Task<bool> AreaNameExistsAsync(string nombre, int? excludeId = null)
    {
        var normalizedName = NormalizeAreaName(nombre);
        var query = _context.Areas.Where(a => a.Nombre == normalizedName);
        
        if (excludeId.HasValue)
        {
            query = query.Where(a => a.Id != excludeId.Value);
        }
        
        return await query.AnyAsync();
    }

    private static string NormalizeAreaName(string nombre)
    {
        if (string.IsNullOrEmpty(nombre))
            return string.Empty;

        // Convert to uppercase
        var normalized = nombre.ToUpperInvariant();
        
        // Remove accents
        var stringBuilder = new StringBuilder();
        var normalizedString = normalized.Normalize(NormalizationForm.FormD);
        
        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }
        
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}