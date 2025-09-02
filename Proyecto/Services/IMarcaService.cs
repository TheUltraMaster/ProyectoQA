using BD.Models;
using Proyecto.DTOS;

namespace Proyecto.Services;

public interface IMarcaService
{
    Task<IEnumerable<Marca>> GetAllMarcasAsync();
    Task<(List<Marca> Marcas, int TotalCount)> GetMarcasPagedAsync(int page, int pageSize, string? search = null);
    Task<Marca?> GetMarcaByIdAsync(int id);
    Task<Marca> CreateMarcaAsync(MarcaDto marcaDto);
    Task<Marca?> UpdateMarcaAsync(int id, MarcaDto marcaDto);
    Task<bool> DeleteMarcaAsync(int id);
    Task<bool> MarcaExistsAsync(int id);
    Task<bool> MarcaNameExistsAsync(string nombre, int? excludeId = null);
}