using BD.Models;
using Proyecto.DTOS;

namespace Proyecto.Services;

public interface IAreaService
{
    Task<IEnumerable<Area>> GetAllAreasAsync();
    Task<(List<Area> Areas, int TotalCount)> GetAreasPagedAsync(int page, int pageSize, string? search = null);
    Task<Area?> GetAreaByIdAsync(int id);
    Task<Area> CreateAreaAsync(AreaDto areaDto);
    Task<Area?> UpdateAreaAsync(int id, AreaDto areaDto);
    Task<bool> DeleteAreaAsync(int id);
    Task<bool> AreaExistsAsync(int id);
    Task<bool> AreaNameExistsAsync(string nombre, int? excludeId = null);
    Task<List<Usuario>> GetUsuariosAsync();
    Task<(List<Usuario> usuarios, int totalCount)> GetUsuariosPaginadosAsync(int page, int pageSize, string? search = null);
}