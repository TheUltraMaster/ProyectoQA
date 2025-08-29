using BD.Data;
using BD.Models;
using Proyecto.DTOS;
using Microsoft.EntityFrameworkCore;
using Proyecto.Mapper;

namespace Proyecto.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly ProyectoContext _context;
        private readonly Map _mapper;

        public EmpleadoService(ProyectoContext context, Map mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(List<Empleado> empleados, int totalCount)> GetEmpleadosPaginadosAsync(int page, int pageSize)
        {
            var totalCount = await _context.Empleados.CountAsync();
            
            var empleados = await _context.Empleados
                .Include(e => e.IdAreaNavigation)
                .Include(e => e.IdUsuarioNavigation)
                .OrderBy(e => e.PrimerNombre)
                .ThenBy(e => e.PrimerApellido)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (empleados, totalCount);
        }

        public async Task<Empleado?> GetEmpleadoByIdAsync(int id)
        {
            return await _context.Empleados
                .Include(e => e.IdAreaNavigation)
                .Include(e => e.IdUsuarioNavigation)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Empleado> CreateEmpleadoAsync(EmpleadoDto empleadoDto)
        {
            var empleado = _mapper.toEntity(empleadoDto);
            empleado.Estado = empleadoDto.Estado ?? "activo";

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            return await GetEmpleadoByIdAsync(empleado.Id) ?? empleado;
        }

        public async Task<Empleado?> UpdateEmpleadoAsync(int id, EmpleadoUpdateDto empleadoDto)
        {
            var existingEmpleado = await _context.Empleados.FindAsync(id);
            if (existingEmpleado == null) return null;

            var updatedEmpleado = _mapper.toEntity(new EmpleadoDto 
            {
                PrimerNombre = empleadoDto.PrimerNombre,
                SegundoNombre = empleadoDto.SegundoNombre,
                PrimerApellido = empleadoDto.PrimerApellido,
                SegundoApellido = empleadoDto.SegundoApellido,
                Estado = empleadoDto.Estado,
                IdArea = empleadoDto.IdArea
            });

            existingEmpleado.PrimerNombre = updatedEmpleado.PrimerNombre;
            existingEmpleado.SegundoNombre = updatedEmpleado.SegundoNombre;
            existingEmpleado.PrimerApellido = updatedEmpleado.PrimerApellido;
            existingEmpleado.SegundoApellido = updatedEmpleado.SegundoApellido;
            existingEmpleado.Estado = empleadoDto.Estado ?? "activo";
            existingEmpleado.IdArea = updatedEmpleado.IdArea;

            await _context.SaveChangesAsync();
            return await GetEmpleadoByIdAsync(id);
        }

        public async Task<bool> DeleteEmpleadoAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null) return false;

            var hasEquipment = await _context.Equipos.AnyAsync(e => e.IdEmpleado == id);
            var hasReports = await _context.Reportes.AnyAsync(r => r.IdEmpleado == id);
            var hasBitacora = await _context.BitacoraEquipos.AnyAsync(b => b.IdEmpleado == id);
            
            if (hasEquipment || hasReports || hasBitacora)
            {
                return false;
            }

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Area>> GetAreasAsync()
        {
            return await _context.Areas
                .OrderBy(a => a.Nombre)
                .ToListAsync();
        }

        public async Task<List<Usuario>> GetUsuariosDisponiblesAsync()
        {
            var usuariosConEmpleado = await _context.Empleados
                .Select(e => e.IdUsuario)
                .ToListAsync();

            return await _context.Usuarios
                .Where(u => !usuariosConEmpleado.Contains(u.Id))
                .OrderBy(u => u.Usuario1)
                .ToListAsync();
        }
    }
}