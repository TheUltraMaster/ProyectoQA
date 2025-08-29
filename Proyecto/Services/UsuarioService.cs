using BD.Data;
using BD.Models;
using Proyecto.DTOS;
using Microsoft.EntityFrameworkCore;
using Bycript;
using Proyecto.Mapper;

namespace Proyecto.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ProyectoContext _context;
        private readonly IBCryptService _bcryptService;
        private readonly Map _mapper;

        public UsuarioService(ProyectoContext context, IBCryptService bcryptService, Map mapper)
        {
            _context = context;
            _bcryptService = bcryptService;
            _mapper = mapper;
        }

        public async Task<(List<Usuario> usuarios, int totalCount)> GetUsuariosPaginadosAsync(int page, int pageSize)
        {
            var totalCount = await _context.Usuarios.CountAsync();
            
            var usuarios = await _context.Usuarios
                .Include(u => u.Empleado)
                .OrderByDescending(u => u.FechaCommit)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (usuarios, totalCount);
        }

        public async Task<Usuario?> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> CreateUsuarioAsync(UsuarioDto usuarioDto)
        {
            var usuario = _mapper.toEntity(usuarioDto);
            
            // Aplicar configuraciones específicas del dominio que el mapper no puede manejar
            usuario.Password = _bcryptService.HashText(usuarioDto.Password);
            usuario.FechaCommit = DateTime.Now;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return await GetUsuarioByIdAsync(usuario.Id) ?? usuario;
        }

        public async Task<Usuario?> UpdateUsuarioAsync(int id, UsuarioDto usuarioDto)
        {
            var existingUsuario = await _context.Usuarios.FindAsync(id);
            if (existingUsuario == null) return null;

            // Usar el mapper para actualizar las propiedades básicas
            var updatedUsuario = _mapper.toEntity(usuarioDto);
            existingUsuario.Usuario1 = updatedUsuario.Usuario1;
            existingUsuario.IsAdmin = updatedUsuario.IsAdmin;
            existingUsuario.Activo = updatedUsuario.Activo; // El mapper ya maneja la conversión bool -> ulong

            // Solo actualizar contraseña si se proporciona una nueva
            if (!string.IsNullOrWhiteSpace(usuarioDto.Password))
            {
                existingUsuario.Password = _bcryptService.HashText(usuarioDto.Password);
            }

            await _context.SaveChangesAsync();
            return await GetUsuarioByIdAsync(id);
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            var hasEmployeeAssociated = await _context.Empleados.AnyAsync(e => e.IdUsuario == id);
            if (hasEmployeeAssociated)
            {
                // No permitir eliminar usuario si tiene empleado asociado
                return false;
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Empleado>> GetEmpleadosDisponiblesAsync()
        {
            // Obtener empleados que no tienen usuario asignado (excepto el usuario temporal "sin_asignar")
            return await _context.Empleados
                .Include(e => e.IdAreaNavigation)
                .Include(e => e.IdUsuarioNavigation)
                .Where(e => e.IdUsuarioNavigation.Usuario1 == "sin_asignar")
                .OrderBy(e => e.PrimerNombre)
                .ThenBy(e => e.PrimerApellido)
                .ToListAsync();
        }

        public async Task<bool> UsuarioExistsAsync(string usuario, int? excludeId = null)
        {
            var query = _context.Usuarios.Where(u => u.Usuario1 == usuario);
            
            if (excludeId.HasValue)
            {
                query = query.Where(u => u.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var usuario = await _context.Usuarios.FindAsync(userId);
            if (usuario == null) return false;

            if (!_bcryptService.VerifyText(currentPassword, usuario.Password))
            {
                return false;
            }

            usuario.Password = _bcryptService.HashText(newPassword);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> AuthenticateAsync(string usuario, string password)
        {
            var user = await _context.Usuarios
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.Usuario1 == usuario);

            if (user == null) return null;

            // Check if user is active
            if (user.Activo == 0UL) return null;

            if (!_bcryptService.VerifyText(password, user.Password))
            {
                return null;
            }

            return user;
        }

        public async Task<bool> ActivateUserAsync(int userId)
        {
            var usuario = await _context.Usuarios.FindAsync(userId);
            if (usuario == null) return false;

            usuario.Activo = 1UL;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateUserAsync(int userId)
        {
            var usuario = await _context.Usuarios.FindAsync(userId);
            if (usuario == null) return false;

            usuario.Activo = 0UL;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(List<Usuario> usuarios, int totalCount)> SearchUsuariosByNameAsync(string searchTerm, int page, int pageSize)
        {
            IQueryable<Usuario> query = _context.Usuarios.Include(u => u.Empleado);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(u => u.Usuario1.Contains(searchTerm) || 
                                   (u.Empleado != null && (u.Empleado.PrimerNombre != null && u.Empleado.PrimerNombre.Contains(searchTerm) ||
                                                           u.Empleado.PrimerApellido != null && u.Empleado.PrimerApellido.Contains(searchTerm))));
            }

            var totalCount = await query.CountAsync();
            
            var usuarios = await query
                .OrderByDescending(u => u.FechaCommit)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (usuarios, totalCount);
        }
    }
}