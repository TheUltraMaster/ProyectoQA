using BD.Data;
using BD.Models;
using Microsoft.EntityFrameworkCore;
using Proyecto.DTOS;
using Proyecto.Mapper;
using System.Globalization;
using System.Text;

namespace Proyecto.Services;

public class MarcaService : IMarcaService
{
    private readonly ProyectoContext _context;
    private readonly Map _mapper;

    public MarcaService(ProyectoContext context, Map mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Marca>> GetAllMarcasAsync()
    {
        return await _context.Marcas
            .Include(m => m.Equipos)
            .OrderBy(m => m.Nombre)
            .ToListAsync();
    }

    public async Task<(List<Marca> Marcas, int TotalCount)> GetMarcasPagedAsync(int page, int pageSize, string? search = null)
    {
        var query = _context.Marcas
            .Include(m => m.Equipos)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(m => m.Nombre.Contains(search));
        }

        var totalCount = await query.CountAsync();

        var marcas = await query
            .OrderBy(m => m.Nombre)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (marcas, totalCount);
    }

    public async Task<Marca?> GetMarcaByIdAsync(int id)
    {
        return await _context.Marcas
            .Include(m => m.Equipos)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Marca> CreateMarcaAsync(MarcaDto marcaDto)
    {
        // Normalize the name: uppercase and remove accents
        marcaDto.Nombre = NormalizeMarcaName(marcaDto.Nombre);
        
        // Check if name already exists
        if (await MarcaNameExistsAsync(marcaDto.Nombre))
        {
            throw new InvalidOperationException("Ya existe una marca con ese nombre");
        }

        var marca = _mapper.toEntity(marcaDto);

        _context.Marcas.Add(marca);
        await _context.SaveChangesAsync();
        
        return await GetMarcaByIdAsync(marca.Id) ?? marca;
    }

    public async Task<Marca?> UpdateMarcaAsync(int id, MarcaDto marcaDto)
    {
        var marca = await _context.Marcas.FindAsync(id);
        if (marca == null)
            return null;

        // Normalize the name: uppercase and remove accents
        marcaDto.Nombre = NormalizeMarcaName(marcaDto.Nombre);
        
        // Check if name already exists (excluding current marca)
        if (await MarcaNameExistsAsync(marcaDto.Nombre, id))
        {
            throw new InvalidOperationException("Ya existe una marca con ese nombre");
        }

        var updatedMarca = _mapper.toEntity(marcaDto);
        marca.Nombre = updatedMarca.Nombre;

        await _context.SaveChangesAsync();
        return await GetMarcaByIdAsync(id);
    }

    public async Task<bool> DeleteMarcaAsync(int id)
    {
        var marca = await _context.Marcas.FindAsync(id);
        if (marca == null)
            return false;

        var hasEquipos = await _context.Equipos.AnyAsync(e => e.IdMarca == id);
        if (hasEquipos)
            return false;

        _context.Marcas.Remove(marca);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarcaExistsAsync(int id)
    {
        return await _context.Marcas.AnyAsync(m => m.Id == id);
    }

    public async Task<bool> MarcaNameExistsAsync(string nombre, int? excludeId = null)
    {
        var normalizedName = NormalizeMarcaName(nombre);
        var query = _context.Marcas.Where(m => m.Nombre == normalizedName);
        
        if (excludeId.HasValue)
        {
            query = query.Where(m => m.Id != excludeId.Value);
        }
        
        return await query.AnyAsync();
    }

    private static string NormalizeMarcaName(string nombre)
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