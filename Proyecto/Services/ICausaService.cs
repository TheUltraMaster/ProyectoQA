using BD.Models;
using Proyecto.DTOS;

namespace Proyecto.Services;

public interface ICausaService
{
    Task<List<CausaDto>> GetAllCausasAsync();
    Task<(List<CausaDto> Items, int TotalCount)> GetCausasPagedAsync(int page, int pageSize);
    Task<CausaDto?> GetCausaByIdAsync(int id);
    Task<CausaDto> CreateCausaAsync(CausaDto causaDto);
    Task<CausaDto?> UpdateCausaAsync(int id, CausaDto causaDto);
    Task<bool> ExistsNombreAsync(string nombre, int? excludeId = null);
}