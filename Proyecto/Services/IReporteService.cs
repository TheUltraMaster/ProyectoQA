using BD.Models;
using Proyecto.DTOS;

namespace Proyecto.Services;

public interface IReporteService
{
    Task<List<ReporteDto>> GetAllReportesAsync();
    Task<(List<ReporteDto> reportes, int totalCount)> GetReportesPaginatedAsync(int page, int pageSize, string? searchEmpleado = null, string? searchEquipo = null);
    Task<ReporteDto?> GetReporteByIdAsync(int id);
    Task<ReporteDto> CreateReporteAsync(ReporteDto reporteDto);
    Task<ReporteDto> UpdateReporteAsync(ReporteDto reporteDto);
    Task<ReporteDto> UpdateReporteWithImageAsync(ReporteDto reporteDto, string? imageUrl);
    Task<List<Causa>> GetAllCausasAsync();
    Task<List<Equipo>> GetAllEquiposAsync();
    Task<List<Empleado>> GetAllEmpleadosAsync();
    Task<List<Equipo>> SearchEquiposAsync(string searchTerm);
    Task<List<Empleado>> SearchEmpleadosAsync(string searchTerm);
    Task<(List<Equipo> equipos, bool hasMore)> SearchEquiposPaginatedAsync(string searchTerm, int page, int pageSize);
    Task<(List<Empleado> empleados, bool hasMore)> SearchEmpleadosPaginatedAsync(string searchTerm, int page, int pageSize);
    Task<string> UploadImageAsync(Stream imageStream, string fileName, string contentType);
    Task<ReporteDto> CreateReporteWithImageAsync(ReporteDto reporteDto, string? imageUrl);
    Task<bool> DeleteImageAsync(string fileName);
    string GetImageFullUrl(string imagePath);
}