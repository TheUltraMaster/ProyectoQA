using BD.Models;
using Proyecto.DTOS;

namespace Proyecto.Services
{
    public interface IUsuarioService
    {
        Task<(List<Usuario> usuarios, int totalCount)> GetUsuariosPaginadosAsync(int page, int pageSize);
        Task<Usuario?> GetUsuarioByIdAsync(int id);
        Task<Usuario> CreateUsuarioAsync(UsuarioDto usuarioDto);
        Task<Usuario?> UpdateUsuarioAsync(int id, UsuarioDto usuarioDto);
        Task<bool> DeleteUsuarioAsync(int id);
        Task<List<Empleado>> GetEmpleadosDisponiblesAsync();
        Task<bool> UsuarioExistsAsync(string usuario, int? excludeId = null);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
        Task<Usuario?> AuthenticateAsync(string usuario, string password);
        Task<bool> ActivateUserAsync(int userId);
        Task<bool> DeactivateUserAsync(int userId);
        Task<(List<Usuario> usuarios, int totalCount)> SearchUsuariosByNameAsync(string searchTerm, int page, int pageSize);
    }
}