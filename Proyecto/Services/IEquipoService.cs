using BD.Models;
using Proyecto.DTOS;

namespace Proyecto.Services
{
    public interface IEquipoService
    {
        Task<(List<EquipoDto> equipos, int totalCount)> GetEquiposPaginadosAsync(int page, int pageSize, TipoEquipo? tipoFiltro = null, int? empleadoFiltro = null, EstadoEquipo? estadoFiltro = null, string? search = null);
        Task<EquipoDto?> GetEquipoByIdAsync(int id);
        Task<Equipo> CreateEquipoAsync(EquipoDto equipoDto, object? specificData = null);
        Task<Equipo?> UpdateEquipoAsync(int id, EquipoDto equipoDto, object? specificData = null);
        Task<bool> DeleteEquipoAsync(int id);
        Task<List<Marca>> GetMarcasAsync();
        Task<List<Empleado>> GetEmpleadosAsync();
        Task<bool> IdentificadorExistsAsync(string? identificador, int? excludeId = null);
        Task<bool> SerieExistsAsync(string serie, int? excludeId = null);
        Task<List<EquipoDto>> GetEquiposByTipoAsync(TipoEquipo tipo);
        Task<List<EquipoDto>> GetEquiposByEmpleadoAsync(int empleadoId);
        Task<bool> CambiarEstadoEquipoAsync(int equipoId, EstadoEquipo nuevoEstado);
        Task<object?> GetSpecificDataAsync(int equipoId, TipoEquipo tipo);

        // Métodos de asignación
        Task<List<EquipoDto>> GetEquiposAsignadosAsync();
        Task<List<EquipoDto>> GetEquiposNoAsignadosAsync();
        Task<bool> AsignarEquiposAsync(List<int> equipoIds, int empleadoId);
        Task<bool> DesasignarEquiposAsync(List<int> equipoIds);
    }
}