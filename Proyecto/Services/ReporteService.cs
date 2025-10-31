using BD.Data;
using BD.Models;
using Microsoft.EntityFrameworkCore;
using Proyecto.DTOS;
using Riok.Mapperly.Abstractions;

namespace Proyecto.Services;

public class ReporteService : IReporteService
{
    private readonly ProyectoContext _context;
    private readonly Bucket.IMinioService _minioService;

    public ReporteService(ProyectoContext context, Bucket.IMinioService minioService)
    {
        _context = context;
        _minioService = minioService;
    }

    public async Task<List<ReporteDto>> GetAllReportesAsync()
    {
        var reportes = await _context.Reportes
            .Include(r => r.IdCausaNavigation)
            .Include(r => r.IdEquipoNavigation)
            .Include(r => r.IdEmpleadoNavigation)
            .Include(r => r.Imagens)
            .ToListAsync();

        return reportes.Select(r => MapReporteToDto(r)).ToList();
    }

    public async Task<(List<ReporteDto> reportes, int totalCount)> GetReportesPaginatedAsync(int page, int pageSize, string? searchEmpleado = null, string? searchEquipo = null)
    {
        var query = _context.Reportes
            .Include(r => r.IdCausaNavigation)
            .Include(r => r.IdEquipoNavigation)
            .Include(r => r.IdEmpleadoNavigation)
            .Include(r => r.Imagens)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchEmpleado))
        {
            query = query.Where(r => r.IdEmpleadoNavigation.PrimerNombre.Contains(searchEmpleado) || 
                                   r.IdEmpleadoNavigation.PrimerApellido.Contains(searchEmpleado));
        }

        if (!string.IsNullOrWhiteSpace(searchEquipo))
        {
            query = query.Where(r => r.IdEquipoNavigation.Identificador.Contains(searchEquipo) || 
                                   r.IdEquipoNavigation.Nombre.Contains(searchEquipo));
        }

        var totalCount = await query.CountAsync();
        
        var reportes = await query
            .OrderByDescending(r => r.FechaCommit)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (reportes.Select(r => MapReporteToDto(r)).ToList(), totalCount);
    }

    public async Task<ReporteDto?> GetReporteByIdAsync(int id)
    {
        var reporte = await _context.Reportes
            .Include(r => r.IdCausaNavigation)
            .Include(r => r.IdEquipoNavigation)
            .Include(r => r.IdEmpleadoNavigation)
            .Include(r => r.Imagens)
            .FirstOrDefaultAsync(r => r.Id == id);

        return reporte != null ? MapReporteToDto(reporte) : null;
    }

    public async Task<ReporteDto> CreateReporteAsync(ReporteDto reporteDto)
    {
        var reporte = new Reporte
        {
            Observacion = reporteDto.Observacion,
            IdCausa = reporteDto.IdCausa,
            IdEquipo = reporteDto.IdEquipo,
            IdEmpleado = reporteDto.IdEmpleado,
            FechaCommit = DateTime.Now
        };

        _context.Reportes.Add(reporte);
        await _context.SaveChangesAsync();

        return await GetReporteByIdAsync(reporte.Id) ?? throw new InvalidOperationException("Error al crear el reporte");
    }

    public async Task<ReporteDto> CreateReporteWithImageAsync(ReporteDto reporteDto, string? imageUrl)
    {
        var reporte = new Reporte
        {
            Observacion = reporteDto.Observacion,
            IdCausa = reporteDto.IdCausa,
            IdEquipo = reporteDto.IdEquipo,
            IdEmpleado = reporteDto.IdEmpleado,
            FechaCommit = DateTime.Now
        };

        _context.Reportes.Add(reporte);
        await _context.SaveChangesAsync();

        if (!string.IsNullOrEmpty(imageUrl))
        {
            var imagen = new Imagen
            {
                Url = imageUrl,
                IdReporte = reporte.Id
            };
            _context.Imagens.Add(imagen);
            await _context.SaveChangesAsync();
        }

        return await GetReporteByIdAsync(reporte.Id) ?? throw new InvalidOperationException("Error al crear el reporte");
    }

    public async Task<ReporteDto> UpdateReporteAsync(ReporteDto reporteDto)
    {
        var reporte = await _context.Reportes.FindAsync(reporteDto.Id);
        if (reporte == null)
            throw new InvalidOperationException("Reporte no encontrado");

        reporte.Observacion = reporteDto.Observacion;
        reporte.IdCausa = reporteDto.IdCausa;
        reporte.IdEquipo = reporteDto.IdEquipo;
        reporte.IdEmpleado = reporteDto.IdEmpleado;

        await _context.SaveChangesAsync();
        return await GetReporteByIdAsync(reporte.Id) ?? throw new InvalidOperationException("Error al actualizar el reporte");
    }

    public async Task<ReporteDto> UpdateReporteWithImageAsync(ReporteDto reporteDto, string? imageUrl)
    {
        var reporte = await _context.Reportes.FindAsync(reporteDto.Id);
        if (reporte == null)
            throw new InvalidOperationException("Reporte no encontrado");

        reporte.Observacion = reporteDto.Observacion;
        reporte.IdCausa = reporteDto.IdCausa;
        reporte.IdEquipo = reporteDto.IdEquipo;
        reporte.IdEmpleado = reporteDto.IdEmpleado;

        if (!string.IsNullOrEmpty(imageUrl))
        {
            var imagen = new Imagen
            {
                Url = imageUrl,
                IdReporte = reporte.Id
            };
            _context.Imagens.Add(imagen);
        }

        await _context.SaveChangesAsync();
        return await GetReporteByIdAsync(reporte.Id) ?? throw new InvalidOperationException("Error al actualizar el reporte");
    }

    public async Task<List<Causa>> GetAllCausasAsync()
    {
        return await _context.Causas.ToListAsync();
    }

    public async Task<List<Equipo>> GetAllEquiposAsync()
    {
        return await _context.Equipos.ToListAsync();
    }

    public async Task<List<Empleado>> GetAllEmpleadosAsync()
    {
        return await _context.Empleados.ToListAsync();
    }

    public async Task<List<Equipo>> SearchEquiposAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllEquiposAsync();

        return await _context.Equipos
            .Where(e => e.Nombre.Contains(searchTerm) || e.Identificador.Contains(searchTerm))
            .OrderBy(e => e.Nombre)
            .ToListAsync();
    }

    public async Task<List<Empleado>> SearchEmpleadosAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllEmpleadosAsync();

        return await _context.Empleados
            .Where(e => e.PrimerNombre.Contains(searchTerm) || 
                       e.SegundoNombre.Contains(searchTerm) ||
                       e.PrimerApellido.Contains(searchTerm) || 
                       e.SegundoApellido.Contains(searchTerm))
            .OrderBy(e => e.PrimerNombre)
            .ThenBy(e => e.PrimerApellido)
            .ToListAsync();
    }

    public async Task<(List<Equipo> equipos, bool hasMore)> SearchEquiposPaginatedAsync(string searchTerm, int page, int pageSize)
    {
        var query = _context.Equipos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(e => e.Nombre.Contains(searchTerm) || e.Identificador.Contains(searchTerm));
        }

        var totalCount = await query.CountAsync();
        var equipos = await query
            .OrderBy(e => e.Nombre)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var hasMore = (page * pageSize) < totalCount;
        return (equipos, hasMore);
    }

    public async Task<(List<Empleado> empleados, bool hasMore)> SearchEmpleadosPaginatedAsync(string searchTerm, int page, int pageSize)
    {
        var query = _context.Empleados.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(e => e.PrimerNombre.Contains(searchTerm) || 
                                   e.SegundoNombre.Contains(searchTerm) ||
                                   e.PrimerApellido.Contains(searchTerm) || 
                                   e.SegundoApellido.Contains(searchTerm));
        }

        var totalCount = await query.CountAsync();
        var empleados = await query
            .OrderBy(e => e.PrimerNombre)
            .ThenBy(e => e.PrimerApellido)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var hasMore = (page * pageSize) < totalCount;
        return (empleados, hasMore);
    }

    public async Task<string> UploadImageAsync(Stream imageStream, string fileName, string contentType)
    {
        return await _minioService.UploadImageAsync(imageStream, fileName, contentType);
    }

    public async Task<bool> DeleteImageAsync(string fileName)
    {
        return await _minioService.DeleteImageAsync(fileName);
    }

    public string GetImageFullUrl(string imagePath)
    {
        return _minioService.GetBucketAddress() + imagePath;
    }

    private ReporteDto MapReporteToDto(Reporte reporte)
    {
        return new ReporteDto
        {
            Id = reporte.Id,
            Observacion = reporte.Observacion,
            IdCausa = reporte.IdCausa,
            IdEquipo = reporte.IdEquipo,
            IdEmpleado = reporte.IdEmpleado,
            NombreEmpleado = reporte.IdEmpleadoNavigation != null ? 
                $"{reporte.IdEmpleadoNavigation.PrimerNombre} {reporte.IdEmpleadoNavigation.PrimerApellido}" : null,
            NombreEquipo = reporte.IdEquipoNavigation?.Nombre,
            NombreCausa = reporte.IdCausaNavigation?.Nombre
        };
    }

    private string GetFileNameFromUrl(string url)
    {
        return url.Contains('/') ? url.Split('/').Last() : url;
    }
}