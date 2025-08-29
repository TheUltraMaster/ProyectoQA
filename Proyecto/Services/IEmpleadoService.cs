using BD.Models;
using Proyecto.DTOS;

namespace Proyecto.Services
{
    public interface IEmpleadoService
    {
        Task<(List<Empleado> empleados, int totalCount)> GetEmpleadosPaginadosAsync(int page, int pageSize);
        Task<Empleado?> GetEmpleadoByIdAsync(int id);
        Task<Empleado> CreateEmpleadoAsync(EmpleadoDto empleadoDto);
        Task<Empleado?> UpdateEmpleadoAsync(int id, EmpleadoUpdateDto empleadoDto);
        Task<bool> DeleteEmpleadoAsync(int id);
        Task<List<Area>> GetAreasAsync();
        Task<List<Usuario>> GetUsuariosDisponiblesAsync();
    }
}