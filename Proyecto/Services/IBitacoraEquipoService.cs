using BD.Models;

namespace Proyecto.Services;

public interface IBitacoraEquipoService
{
    Task<IEnumerable<BitacoraEquipo>> GetAllAsync();
    Task<IEnumerable<BitacoraEquipo>> GetFilteredAsync(string? empleadoNombre = null, string? equipoIdentificador = null, string? equipoNombre = null);
    Task<BitacoraEquipo?> GetByIdAsync(int id);
    Task<IEnumerable<BitacoraEquipo>> GetByUserAreasAsync(int userId, int? areaId = null);
    Task<IEnumerable<Area>> GetUserAreasAsync(int userId);
    Task<(List<BitacoraEquipo> BitacoraEquipos, int TotalCount)> GetBitacoraEquiposPagedAsync(int page, int pageSize, string? empleadoNombre = null, string? equipoNombre = null, string? equipoIdentificador = null, bool? asignado = null, DateTime? fechaInicio = null, DateTime? fechaFin = null);
    Task<IEnumerable<Empleado>> GetEmpleadosForFilterAsync(int page = 1, int pageSize = 50, string? search = null);
    Task<IEnumerable<Equipo>> GetEquiposForFilterAsync(int page = 1, int pageSize = 50, string? search = null);
}