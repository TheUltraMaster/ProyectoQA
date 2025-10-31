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

        public async Task<IEnumerable<Empleado>> GetAllEmpleadosAsync()
        {
            return await _context.Empleados
                .Include(e => e.IdAreaNavigation)
                .Include(e => e.IdUsuarioNavigation)
                .OrderBy(e => e.PrimerNombre)
                .ThenBy(e => e.PrimerApellido)
                .ToListAsync();
        }

        public async Task<(List<Empleado> Empleados, int TotalCount)> GetEmpleadosPagedAsync(int page, int pageSize, string? search = null)
        {
            var query = _context.Empleados
                .Include(e => e.IdAreaNavigation)
                .Include(e => e.IdUsuarioNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(e => 
                    (e.PrimerNombre + " " + e.SegundoNombre + " " + e.PrimerApellido + " " + e.SegundoApellido).Contains(search) ||
                    (e.IdAreaNavigation != null && e.IdAreaNavigation.Nombre.Contains(search)) ||
                    (e.IdUsuarioNavigation != null && e.IdUsuarioNavigation.Usuario1.Contains(search)));
            }

            var totalCount = await query.CountAsync();

            var empleados = await query
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
            if (empleadoDto.IdUsuario.HasValue)
            {
                var userAlreadyAssigned = await _context.Empleados
                    .AnyAsync(e => e.IdUsuario == empleadoDto.IdUsuario);
                
                if (userAlreadyAssigned)
                    throw new InvalidOperationException("El usuario ya está asignado a otro empleado.");
            }

            var empleado = _mapper.toEntity(empleadoDto);
            empleado.Estado = empleadoDto.Estado ?? "activo";
            empleado.IdUsuario = empleadoDto.IdUsuario;

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            return await GetEmpleadoByIdAsync(empleado.Id) ?? empleado;
        }

        public async Task<Empleado?> UpdateEmpleadoAsync(int id, EmpleadoDto empleadoDto)
        {
            var existingEmpleado = await _context.Empleados.FindAsync(id);
            if (existingEmpleado == null) return null;

            if (empleadoDto.IdUsuario.HasValue && empleadoDto.IdUsuario != existingEmpleado.IdUsuario)
            {
                var userAlreadyAssigned = await _context.Empleados
                    .AnyAsync(e => e.IdUsuario == empleadoDto.IdUsuario && e.Id != id);
                
                if (userAlreadyAssigned)
                    throw new InvalidOperationException("El usuario ya está asignado a otro empleado.");
            }

            var updatedEmpleado = _mapper.toEntity(empleadoDto);

            existingEmpleado.PrimerNombre = updatedEmpleado.PrimerNombre;
            existingEmpleado.SegundoNombre = updatedEmpleado.SegundoNombre;
            existingEmpleado.PrimerApellido = updatedEmpleado.PrimerApellido;
            existingEmpleado.SegundoApellido = updatedEmpleado.SegundoApellido;
            existingEmpleado.Estado = updatedEmpleado.Estado ?? "activo";
            existingEmpleado.IdArea = updatedEmpleado.IdArea;
            existingEmpleado.IdUsuario = updatedEmpleado.IdUsuario;

            await _context.SaveChangesAsync();
            return await GetEmpleadoByIdAsync(id);
        }


        public async Task<List<Area>> GetAreasAsync()
        {
            return await _context.Areas
                .OrderBy(a => a.Nombre)
                .ToListAsync();
        }

        public async Task<(List<Area> Areas, int TotalCount)> GetAreasPagedAsync(int page, int pageSize, string? search = null)
        {
            var query = _context.Areas.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.Nombre.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var areas = await query
                .OrderBy(a => a.Nombre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (areas, totalCount);
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

        public async Task<List<Usuario>> GetUsuariosDisponiblesAsync(int empleadoId)
        {
            var usuariosConEmpleado = await _context.Empleados
                .Where(e => e.Id != empleadoId)
                .Select(e => e.IdUsuario)
                .ToListAsync();

            return await _context.Usuarios
                .Where(u => !usuariosConEmpleado.Contains(u.Id))
                .OrderBy(u => u.Usuario1)
                .ToListAsync();
        }

        public async Task<(List<Usuario> Usuarios, int TotalCount)> GetUsuariosDisponiblesPagedAsync(int page, int pageSize, string? search = null)
        {
            var usuariosConEmpleado = await _context.Empleados
                .Select(e => e.IdUsuario)
                .ToListAsync();

            var query = _context.Usuarios
                .Where(u => !usuariosConEmpleado.Contains(u.Id))
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.Usuario1.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var usuarios = await query
                .OrderBy(u => u.Usuario1)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (usuarios, totalCount);
        }

        public async Task<(List<Usuario> Usuarios, int TotalCount)> GetUsuariosDisponiblesPagedAsync(int empleadoId, int page, int pageSize, string? search = null)
        {
            var usuariosConEmpleado = await _context.Empleados
                .Where(e => e.Id != empleadoId)
                .Select(e => e.IdUsuario)
                .ToListAsync();

            var query = _context.Usuarios
                .Where(u => !usuariosConEmpleado.Contains(u.Id))
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.Usuario1.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var usuarios = await query
                .OrderBy(u => u.Usuario1)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (usuarios, totalCount);
        }

        public async Task<bool> EmpleadoExistsAsync(int id)
        {
            return await _context.Empleados.AnyAsync(e => e.Id == id);
        }

        public List<string> GetEstadosEmpleado()
        {
            return EnumExtensions.GetAllDescriptions<EstadoEmpleado>();
        }
    }
}