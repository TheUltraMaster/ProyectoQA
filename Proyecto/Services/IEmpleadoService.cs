using BD.Models;
using Proyecto.DTOS;

namespace Proyecto.Services
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<Empleado>> GetAllEmpleadosAsync();
        Task<(List<Empleado> Empleados, int TotalCount)> GetEmpleadosPagedAsync(int page, int pageSize, string? search = null);
        Task<Empleado?> GetEmpleadoByIdAsync(int id);
        Task<Empleado> CreateEmpleadoAsync(EmpleadoDto empleadoDto);
        Task<Empleado?> UpdateEmpleadoAsync(int id, EmpleadoDto empleadoDto);
        Task<bool> EmpleadoExistsAsync(int id);
        Task<List<Area>> GetAreasAsync();
        Task<(List<Area> Areas, int TotalCount)> GetAreasPagedAsync(int page, int pageSize, string? search = null);
        Task<List<Usuario>> GetUsuariosDisponiblesAsync();
        Task<List<Usuario>> GetUsuariosDisponiblesAsync(int empleadoId);
        Task<(List<Usuario> Usuarios, int TotalCount)> GetUsuariosDisponiblesPagedAsync(int page, int pageSize, string? search = null);
        Task<(List<Usuario> Usuarios, int TotalCount)> GetUsuariosDisponiblesPagedAsync(int empleadoId, int page, int pageSize, string? search = null);
        List<string> GetEstadosEmpleado();
    }
}