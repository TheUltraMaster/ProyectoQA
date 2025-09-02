using BD.Data;
using BD.Models;
using Microsoft.EntityFrameworkCore;
using Proyecto.DTOS;
using Proyecto.Mapper;

namespace Proyecto.Services;

public class CausaService : ICausaService
{
    private readonly ProyectoContext _context;
    private readonly Map _mapper;

    public CausaService(ProyectoContext context, Map mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CausaDto>> GetAllCausasAsync()
    {
        var causas = await _context.Causas.ToListAsync();
        return causas.Select(c => _mapper.CausaToDto(c)).ToList();
    }

    public async Task<CausaDto?> GetCausaByIdAsync(int id)
    {
        var causa = await _context.Causas.FindAsync(id);
        return causa != null ? _mapper.CausaToDto(causa) : null;
    }

    public async Task<CausaDto> CreateCausaAsync(CausaDto causaDto)
    {
        // Convert to uppercase
        causaDto.Nombre = causaDto.Nombre.ToUpper();
        
        // Check if name already exists
        if (await ExistsNombreAsync(causaDto.Nombre))
        {
            throw new InvalidOperationException("Ya existe una causa con ese nombre");
        }

        var causa = _mapper.DtoToCausa(causaDto);
        
        _context.Causas.Add(causa);
        await _context.SaveChangesAsync();

        return _mapper.CausaToDto(causa);
    }

    public async Task<CausaDto?> UpdateCausaAsync(int id, CausaDto causaDto)
    {
        var causa = await _context.Causas.FindAsync(id);
        if (causa == null)
            return null;

        // Convert to uppercase
        causaDto.Nombre = causaDto.Nombre.ToUpper();
        
        // Check if name already exists (excluding current record)
        if (await ExistsNombreAsync(causaDto.Nombre, id))
        {
            throw new InvalidOperationException("Ya existe una causa con ese nombre");
        }

        causa.Nombre = causaDto.Nombre;
        causa.Descripcion = causaDto.Descripcion;

        await _context.SaveChangesAsync();
        return _mapper.CausaToDto(causa);
    }

    public async Task<(List<CausaDto> Items, int TotalCount)> GetCausasPagedAsync(int page, int pageSize)
    {
        var totalCount = await _context.Causas.CountAsync();
        
        var causas = await _context.Causas
            .Include(c => c.Reportes)
            .OrderBy(c => c.Nombre)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            
        var causaDtos = causas.Select(c => _mapper.CausaToDto(c)).ToList();
        
        return (causaDtos, totalCount);
    }

    public async Task<bool> ExistsNombreAsync(string nombre, int? excludeId = null)
    {
        var query = _context.Causas.Where(c => c.Nombre.ToUpper() == nombre.ToUpper());
        
        if (excludeId.HasValue)
        {
            query = query.Where(c => c.Id != excludeId.Value);
        }
        
        return await query.AnyAsync();
    }
}